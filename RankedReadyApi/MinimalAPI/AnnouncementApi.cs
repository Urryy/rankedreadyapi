using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.Announcement;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI
{
    public static class AnnouncementApi
    {
        private static string ENDPOINT_V1 = "api/v1/announcement";

        public static void RegisterAnnouncementApi(this WebApplication app)
        {
            app.MapGet($"{ENDPOINT_V1}/all", AnnouncementHandler.GetAllAnnouncments)
                .RequireAuthorization();

            app.MapGet($"{ENDPOINT_V1}/all/{{objectId:Guid}}", AnnouncementHandler.GetAnnouncment)
                .RequireAuthorization();

            app.MapPost($"{ENDPOINT_V1}/create", AnnouncementHandler.CreateAnnouncement)
                .RequireAuthorization()
                .AddEndpointFilter<ValidationFilterAttribute<AnnouncementModel>>();

            app.MapPost($"{ENDPOINT_V1}/update/{{objectId:Guid}}", AnnouncementHandler.UpdateAnnouncement)
                .RequireAuthorization()
                .AddEndpointFilter<ValidationFilterAttribute<AnnouncementModel>>();

            app.MapDelete($"{ENDPOINT_V1}/all/{{objectId:Guid}}", AnnouncementHandler.DeleteAnnouncment)
                .RequireAuthorization();
        }
    }
}
