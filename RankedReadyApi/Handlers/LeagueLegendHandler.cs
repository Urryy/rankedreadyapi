using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Attributes;
using RankedReadyApi.Business.Accessors;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.LeagueLegend;
using RankedReadyApi.Common.Models.Sorting;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReadyApi.Handlers;

public static class LeagueLegendHandler
{
    public static async Task<IResult> GetAvailableAccounts([FromBody] SortingPanel panel,
        ILeagueLegendAccountService srvcLeagueLegend)
    {
        var accounts = await srvcLeagueLegend.GetLeagueLegendAccounts(panel);
        return Results.Json(accounts);
    }

    public static async Task<IResult> GetDetailsAboutAccount([FromQuery] Guid objectId,
        ILeagueLegendAccountService srvcLeagueLegend, ICurrentUserAccessor accessor,
        IUserService srvcUser)
    {
        var userId = accessor.GetCurrentUserId();
        if (string.IsNullOrEmpty(userId) || Guid.TryParse(userId, out var id))
            return Results.Unauthorized();

        var user = await srvcUser.GetAsync(id);
        var account = await srvcLeagueLegend.GetFullAccountById(objectId);

        if (user.Id.ToEnum<Role>() != Role.User)
            return Results.Json(account);

        if (user.Id == account.UserId)
            return Results.Json(account);

        return Results.Json(await srvcLeagueLegend.GetAccountWithoutCredById(objectId));
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> GetAllAccounts(SortingPanel panel,
        ILeagueLegendAccountService srvcLeagueLegend)
    {
        var accounts = await srvcLeagueLegend.GetAllLeagueLegendAccounts(panel);
        return Results.Json(accounts);
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> CreateAccount([FromBody] LeagueLegendAccountModel model,
        ILeagueLegendAccountService srvcLeagueLegend)
    {
        await srvcLeagueLegend.CreateLeagueLegendAcccount(model);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> UpdateAccount([FromBody] LeagueLegendAccountModel model,
        [FromRoute] Guid objectId,
        ILeagueLegendAccountService srvcLeagueLegend)
    {
        if (objectId == Guid.Empty)
            return Results.BadRequest("Not recognized account id");

        await srvcLeagueLegend.UpdateLeagueLegendAcccount(model, objectId);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> DeleteAccount([FromRoute] Guid objectId,
        ILeagueLegendAccountService srvcLeagueLegend)
    {
        if (objectId == Guid.Empty)
            return Results.BadRequest("Not recognized account id");

        await srvcLeagueLegend.DeleteLeagueLegendAcccount(objectId);
        return Results.Ok();
    }
}
