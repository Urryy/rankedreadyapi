using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RankedReadyApi.Accessors;
using RankedReadyApi.Business.Accessors;
using RankedReadyApi.Business.Service.Implementations;
using RankedReadyApi.Business.Service.Interfaces;
using System.Reflection;

namespace RankedReadyApi.CrossCutting.IoC.InversionDependency;

public static class InjectBusiness
{
    public static WebApplicationBuilder ConnectBusiness(this WebApplicationBuilder builder)
    {
        builder.Services.InjectEmailServices();
        builder.Services.InjectServices();
        builder.Services.InjectAccessors();
        builder.Services.InjectLogging();
        builder.Services.AddControllers();
        return builder;
    }

    private static void InjectEmailServices(this IServiceCollection services)
    {
        services.AddFluentEmail("rankedready@gmail.com")
            .AddSmtpSender("smtp.gmail.com", 587, "yatsko19791@gmail.com", "huij qmww ywgk azba");
    }

    private static void InjectServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadServiceAsync<,>), typeof(ReadServiceAsync<,>));
        services.AddScoped(typeof(IGenericServiceAsync<,>), typeof(GenericServiceAsync<,>));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAnnouncementService, AnnouncementService>();
        services.AddScoped<ILeagueLegendAccountService, LeagueLegendAccountService>();
        services.AddScoped<ISkinService, SkinService>();
        services.AddScoped<ITransactionStripeService, TransactionStripeService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IValorantAccountService, ValorantAccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICodeService, CodeService>();
        services.AddScoped<ISupportTicketService, SupportTicketService>();
    }

    private static void InjectAccessors(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
    }

    public static void InjectLogging(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddSingleton(typeof(ILogger), typeof(Logger<Assembly>));
    }
}
