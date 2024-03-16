using Microsoft.AspNetCore.Builder;
using RankedReadyApi.Middlewares;
using Stripe;

namespace RankedReadyApi.CrossCutting.IoC.BuilderApplication
{
    public static class ConfigureApp
    {
        public static WebApplication ConfigureApplication(this WebApplication app)
        {
            app.InjectMiddlewares();
            return app;
        }

        public static void InjectMiddlewares(this WebApplication app)
        {
            app.UseCors(opt => opt.WithOrigins("http://localhost:4000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true));

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();

            #region Static Files
            //app.UseFileServer(new FileServerOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(SystemService.GetApplicationFolder(), "SkinsImages")),
            //    RequestPath = "/SkinsImages",
            //    EnableDefaultFiles = false,
            //    EnableDirectoryBrowsing = true

            //});

            //app.UseFileServer(new FileServerOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(SystemService.GetApplicationFolder(), "AnnouncementPreviews")),
            //    RequestPath = "/AnnouncementPreviews",
            //    EnableDefaultFiles = false,
            //    EnableDirectoryBrowsing = true
            //});
            #endregion

            app.UseRouting();

            StripeConfiguration.ApiKey = app.Configuration["Stripe:SecretKey"];

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
        }
    }
}
