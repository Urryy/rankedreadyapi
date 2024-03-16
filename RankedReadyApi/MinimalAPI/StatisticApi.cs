using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.Statistic;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI;

public static class StatisticApi
{
    private static string ENDPOINT_V1 = "api/v1/skin";

    public static void RegisterStatisticApi(this WebApplication app)
    {
        app.MapGet($"{ENDPOINT_V1}/ordersreceived", StatisticHandler.GetOrdersRecieved)
            .RequireAuthorization();

        app.MapGet($"{ENDPOINT_V1}/availableaccounts", StatisticHandler.GetAvailableAccounts)
            .RequireAuthorization();

        app.MapGet($"{ENDPOINT_V1}/users", StatisticHandler.GetRegistredUsers)
            .RequireAuthorization();

        app.MapGet($"{ENDPOINT_V1}/profit", StatisticHandler.GetProfit)
            .RequireAuthorization();

        app.MapPost($"{ENDPOINT_V1}/periodusers", StatisticHandler.GetUserByPeriod)
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilterAttribute<PeriodSortingModel>>();
    }
}
