using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.Transaction;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI;

public static class TransactionApi
{
    private static string ENDPOINT_V1 = "api/v1/transaction";
    private static string ENDPOINT_V1_PAYTABS = "api/v1/transaction/paytabs";
    private static string ENDPOINT_V1_STRIPE = "api/v1/transaction/stripe";

    public static void RegisterTransactionApi(this WebApplication app)
    {
        app.MapGet($"{ENDPOINT_V1}", TransactionHandler.GetAllTransactions)
            .RequireAuthorization();

        app.MapGet($"{ENDPOINT_V1}/export", TransactionHandler.ExportToExcel)
            .RequireAuthorization();

        app.MapGet($"{ENDPOINT_V1}/profile/user", TransactionHandler.GetTransactionsByUser)
            .RequireAuthorization();

        app.MapGet($"{ENDPOINT_V1}/details/{{objectId:Guid}}", TransactionHandler.GetDetailTransaction)
            .RequireAuthorization();

        app.MapDelete($"{ENDPOINT_V1}/delete/{{objectId:Guid}}", TransactionHandler.DeleteTransaction)
            .RequireAuthorization();

        app.MapPost($"{ENDPOINT_V1_PAYTABS}/create", TransactionHandler.CreatePaytabsTransaction)
            .AddEndpointFilter<ValidationFilterAttribute<TransactionModel>>();

        app.MapPost($"{ENDPOINT_V1_PAYTABS}/return", TransactionHandler.ReturnPaytabs);

        app.MapPost($"{ENDPOINT_V1_PAYTABS}/webhook", TransactionHandler.WebhookPaytabs);

        app.MapPost($"{ENDPOINT_V1_STRIPE}/create", TransactionHandler.CreateStripeTransaction)
            .AddEndpointFilter<ValidationFilterAttribute<TransactionModel>>();

        app.MapPost($"{ENDPOINT_V1_STRIPE}/checkout/confirmation", TransactionHandler.ConfirmationStripePayment);
    }
}
