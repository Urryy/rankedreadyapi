using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Attributes;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.Statistic;
using RankedReadyApi.Common.Models.User;

namespace RankedReadyApi.Handlers;

public static class StatisticHandler
{
    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> GetOrdersRecieved(ITransactionService srvcTransaction)
    {
        var allOrders = await srvcTransaction.GetCountSuccededTransactions();
        return Results.Json(new { OrdersRecieved = allOrders });
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> GetAvailableAccounts(ILeagueLegendAccountService srvcLeagueLegend,
        IValorantAccountService srvcValorant)
    {
        var leagueCount = await srvcLeagueLegend.GetCountAvailableAccs();
        var valorantCount = await srvcValorant.GetCountAvailableAccs();
        return Results.Json(new { AvailableAccounts = (leagueCount + valorantCount) });
    }


    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> GetRegistredUsers(IUserService srvcUser)
    {
        var users = await srvcUser.GetUsers();
        return Results.Json(new { RegisteredUsers = users.Count() });
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> GetProfit(ITransactionService srvcTransaction)
    {
        var profit = await srvcTransaction.GetProfit();
        return Results.Json(profit);
    }

    [Permission(Role.Admin, Role.SuperManager, Role.Manager)]
    public static async Task<IResult> GetUserByPeriod([FromBody] PeriodSortingModel sort,
        IUserService srvcUser)
    {
        var model = new List<PeriodUsersModel>();
        switch (sort.PeriodSort)
        {
            case "Week":
                model = await srvcUser.GetUsersByPeriod(DateTime.Now.AddDays(-7));
                break;
            case "Year":
                model = await srvcUser.GetUsersByPeriod(DateTime.Now.AddYears(-1));
                break;
            case "Month":
                model = await srvcUser.GetUsersByPeriod(DateTime.Now.AddMonths(-1));
                break;
            case "WholePeriod":
                model = await srvcUser.GetUsersByPeriod(DateTime.Now.AddYears(-3));
                break;
        }

        return Results.Json(model);
    }
}
