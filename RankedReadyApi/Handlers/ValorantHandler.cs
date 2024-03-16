using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Attributes;
using RankedReadyApi.Business.Accessors;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.Sorting;
using RankedReadyApi.Common.Models.Valorant;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReadyApi.Handlers;

public class ValorantHandler
{
    public static async Task<IResult> GetAvailableAccounts([FromBody] SortingPanel panel,
        IValorantAccountService srvcValorant)
    {
        var accounts = await srvcValorant.GetValorantAccounts(panel);
        return Results.Json(accounts);
    }

    public static async Task<IResult> GetDetailsAboutAccount([FromQuery] Guid objectId,
        IValorantAccountService srvcValorant, ICurrentUserAccessor accessor,
        IUserService srvcUser)
    {
        var userId = accessor.GetCurrentUserId();
        if (string.IsNullOrEmpty(userId) || Guid.TryParse(userId, out var id))
            return Results.Unauthorized();

        var user = await srvcUser.GetAsync(id);
        var account = await srvcValorant.GetFullAccountById(objectId);

        if (user.Id.ToEnum<Role>() != Role.User)
            return Results.Json(account);

        if (user.Id == account.UserId)
            return Results.Json(account);

        return Results.Json(await srvcValorant.GetAccountWithoutCredById(objectId));
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> GetAllAccounts([FromBody] SortingPanel panel,
        IValorantAccountService srvcValorant)
    {
        var accounts = await srvcValorant.GetAllValorantAccounts(panel);
        return Results.Json(accounts);
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> CreateAccount([FromBody] ValorantAccountModel model,
        IValorantAccountService srvcValorant)
    {
        await srvcValorant.CreateValorantAcccount(model);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> UpdateAccount([FromBody] ValorantAccountModel model,
        [FromRoute] Guid objectId,
        IValorantAccountService srvcValorant)
    {
        if (objectId == Guid.Empty)
            return Results.BadRequest("Not recognized account id");

        await srvcValorant.UpdateValorantAcccount(model, objectId);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> DeleteAccount([FromRoute] Guid objectId,
        IValorantAccountService srvcValorant)
    {
        if (objectId == Guid.Empty)
            return Results.BadRequest("Not recognized account id");

        await srvcValorant.DeleteValorantAcccount(objectId);
        return Results.Ok();
    }
}
