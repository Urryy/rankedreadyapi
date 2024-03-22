using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using Winton.Extensions.Configuration.Consul;

namespace RankedReadyApi.Configure;

public static class WebAppConfigure
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        string host = Environment.GetEnvironmentVariable("HOST")!;
        if (!string.IsNullOrEmpty(host))
            builder.Configuration.AddConsul(
                $"Appsettings/RankedReadyApi/appsettings.json",
                options =>
                {
                    options.Optional = true;
                    options.ReloadOnChange = true;
                    options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                    options.ConsulConfigurationOptions = cco => { cco.Address = new Uri(host); };
                    options.OnLoadException = (consulLoadExceptionContext) =>
                    {
                        Console.WriteLine($"Error onLoadException {consulLoadExceptionContext.Exception.Message} and stacktrace {consulLoadExceptionContext.Exception.StackTrace}");
                        throw consulLoadExceptionContext.Exception;
                    };
                    options.OnWatchException = (consulWatchExceptionContext) =>
                    {
                        Console.WriteLine($"Unable to watchChanges in Consul due to {consulWatchExceptionContext.Exception.Message}");
                        return TimeSpan.FromSeconds(2);
                    };
                }
            );
        else
            builder.Configuration.AddUserSecrets<Program>();

        builder.WebHost.ConfigureKestrel(options =>
        {
            //Add HTTP/3 if if we work with gRPC protocls
            //options.Listen(IPAddress.Any, 5002, listenOptions =>
            //{
            //    listenOptions.Protocols = HttpProtocols.Http3;
            //});
            options.Listen(IPAddress.Any, 5001, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
            options.Listen(IPAddress.Any, 5000, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });
        })
        .UseKestrel(options => { options.Limits.MaxRequestBodySize = 15_000_000; })
        .UseIISIntegration()
        .UseIIS();

        return builder;
    }
}
