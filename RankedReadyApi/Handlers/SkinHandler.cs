using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Attributes;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.Skin;

namespace RankedReadyApi.Handlers;

public class SkinHandler
{
    public static async Task<IResult> GetSkins(
        [FromQuery] string? name,
        ISkinService srvcSkin)
    {
        var skins = await srvcSkin.GetSkins();
        if (skins.Count() > 0 && !string.IsNullOrEmpty(name))
        {
            skins = skins.Where(i => i.ChampionName.Contains(name));
        }

        return Results.Json(skins);
    }

    public static async Task<IResult> GetSkin(
        [FromRoute] Guid objectId,
        ISkinService srvcSkin)
    {
        var skin = await srvcSkin.GetSkin(objectId);
        return Results.Json(skin);
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> DeleteSkin(
        [FromRoute] Guid objectId,
        ISkinService srvcSkin)
    {
        await srvcSkin.DeleteSkin(objectId);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> CreateSkin(
        SkinModel model,
        HttpContext httpContext,
        ISkinService srvcSkin)
    {
        string fileSrc = string.Format("{0}://{1}{2}/{3}", httpContext.Request.Scheme, httpContext.Request.Host, httpContext.Request.PathBase, model.Skin.FileName);
        await srvcSkin.CreateSkin(model, fileSrc);
        return Results.Ok();
    }
}
