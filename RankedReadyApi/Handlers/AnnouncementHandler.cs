using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Attributes;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.Announcement;

namespace RankedReadyApi.Handlers;

public static class AnnouncementHandler
{
    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> GetAllAnnouncments(
        IAnnouncementService srvcAnnouncement)
    {
        var announcements = await srvcAnnouncement.GetAnnouncements();
        return Results.Json(announcements);
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> CreateAnnouncement(
        AnnouncementModel model,
        IAnnouncementService srvcAnnouncement)
    {
        await srvcAnnouncement.CreateAnnouncement(model);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> UpdateAnnouncement(
        [FromBody] AnnouncementModel model,
        [FromRoute] Guid objectId,
        IAnnouncementService srvcAnnouncement)
    {
        await srvcAnnouncement.UpdateAnnouncement(model, objectId);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> GetAnnouncment(
       [FromRoute] Guid objectId,
       IAnnouncementService srvcAnnouncement)
    {
        if (objectId == Guid.Empty)
            throw new NullReferenceException("ObjectId isn't correct");

        await srvcAnnouncement.GetAnnouncement(objectId);
        return Results.Json(srvcAnnouncement);
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> DeleteAnnouncment(
       [FromRoute] Guid objectId,
       IAnnouncementService srvcAnnouncement)
    {
        if (objectId == Guid.Empty)
            throw new NullReferenceException("ObjectId isn't correct");

        await srvcAnnouncement.DeleteAnnouncement(objectId);
        return Results.Json(srvcAnnouncement);
    }
}
