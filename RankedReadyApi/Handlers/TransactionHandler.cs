using Microsoft.AspNetCore.Mvc;
using RankedReady.DataAccess.Extensions;
using RankedReadyApi.Attributes;
using RankedReadyApi.Business.Accessors;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.Transaction;
using RankedReadyApi.Common.Models.User;
using Stripe.Checkout;

namespace RankedReadyApi.Handlers;

public static class TransactionHandler
{
    public static async Task<IResult> GetAllTransactions([FromQuery] string? searchTerm,
        ITransactionService srvcTransaction)
    {
        var transactions = await srvcTransaction.GetAllTransactions();
        if (!string.IsNullOrEmpty(searchTerm))
            transactions = transactions.Where(i => i.Email.Contains(searchTerm) || i.Id.ToString().Contains(searchTerm));

        return Results.Json(transactions);
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> ExportToExcel(ITransactionService srvcTransaction)
    {
        var ms = await srvcTransaction.ExportToExcel();
        return Results.File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Transactions.xlsx");
    }

    public static async Task<IResult> GetTransactionsByUser(ICurrentUserAccessor accessor,
        ITransactionService srvcTransaction)
    {
        var userId = accessor.GetCurrentUserId();

        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
            return Results.Unauthorized();

        var transactions = await srvcTransaction.GetUserTransactions(id);
        return Results.Json(transactions);
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> GetDetailTransaction([FromRoute] Guid objectId,
        ITransactionService srvcTransaction)
    {
        if (objectId == Guid.Empty)
        {
            throw new NullReferenceException("ObjectId isn't correct");
        }

        var transaction = await srvcTransaction.GetTransactionDetails(objectId);

        return Results.Json(transaction);
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> DeleteTransaction([FromRoute] Guid objectId,
        ITransactionService srvcTransaction)
    {
        if (objectId == Guid.Empty)
        {
            throw new NullReferenceException("ObjectId isn't correct");
        }

        await srvcTransaction.DeleteTransaction(objectId);

        return Results.Ok();
    }

    public static async Task<IResult> CreatePaytabsTransaction(TransactionModel model,
        ICurrentUserAccessor accessor, ITransactionService srvcTransaction,
        IUserService srvcUser, IConfiguration configuration)
    {
        var userId = accessor.GetCurrentUserId();

        var accessToken = string.Empty;
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var usrId))
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                var user = await srvcUser.RegisterByTransaction(new RegisterUserModel { Email = model.Email, Password = "12345678", RepeatPassword = "12345678" });
                var accessTokenFromUser = await srvcUser.Login(new LoginUserModel { Email = model.Email, Password = "12345678" });
                accessToken = accessTokenFromUser.Token;
                usrId = Guid.Parse(user.Id);
            }
            else
            {
                return Results.BadRequest("User doesn't exist");
            }
        }

        model.UserId = usrId;
        model.returnUrl = $"{configuration["domain"]}/api/Transaction/paytabs/Return";
        model.callbackUrl = $"{configuration["domain"]}/api/Transaction/paytabs/Webhook";
        var transaction = await srvcTransaction.CreateTransaction(model);

        if (transaction is null)
        {
            return Results.BadRequest("Transaction not created, something went wrong");
        }

        var payment = await srvcTransaction.Pay(Guid.Parse(transaction.Id));
        if (payment == null)
            return Results.BadRequest("Transaction doesn't create");

        return Results.Json(new { redirect = payment.redirect_url, token = accessToken });
    }

    //For debug
    public static async Task<IResult> ReturnPaytabs(Transaction_Result content,
    ITransactionService srvcTransaction)
    {
        var result = await srvcTransaction.ReturnCallbackTransaction(content);
        if (result)
        {
            return Results.Redirect("https://rankedready.com/orderSent");
        }

        return Results.Redirect("https://rankedready.com/shopLol");
    }

    //For Release
    public static async Task<IResult> WebhookPaytabs(Transaction_Result content,
        ITransactionService srvcTransaction)
    {
        var result = await srvcTransaction.ReturnCallbackTransaction(content);
        if (result)
        {
            return Results.Redirect("https://rankedready.com/orderSent");
        }

        return Results.Redirect("https://rankedready.com/shopLol");
    }

    public static async Task<IResult> CreateStripeTransaction([FromBody] TransactionModel model,
        ILeagueLegendAccountService srvcLeagueLegend, IValorantAccountService srvcValorant,
        HttpContext httpContext, ICurrentUserAccessor accessor, IUserService srvcUser,
        ITransactionService srvcTransaction)
    {
        var accountValorant = await srvcValorant.GetNullableAccountById(model.AccountId);
        var accountLeagueLegend = await srvcLeagueLegend.GetNullableAccountById(model.AccountId);

        var options = new SessionCreateOptions
        {
            SuccessUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}" + "/api/transaction/stripe/" + "checkout/confirmation?session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}" + "/api/transaction/stripe/" + "checkout/confirmation?session_id={CHECKOUT_SESSION_ID}",
            Mode = "payment",
            LineItems = new List<SessionLineItemOptions>()
        };

        options.CreateSessionOptions(accountValorant, accountLeagueLegend);

        var srvc = new SessionService();
        Session s = srvc.Create(options);

        var userId = accessor.GetCurrentUserId();

        var accessToken = string.Empty;
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var usrId))
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                var user = await srvcUser.RegisterByTransaction(new RegisterUserModel { Email = model.Email, Password = "12345678", RepeatPassword = "12345678" });
                var accessTokenFromUser = await srvcUser.Login(new LoginUserModel { Email = model.Email, Password = "12345678" });
                accessToken = accessTokenFromUser.Token;
                usrId = Guid.Parse(user.Id);
            }
            else
            {
                return Results.BadRequest("User doesn't exist");
            }
        }

        model.UserId = usrId;

        var trStripe = await srvcTransaction.CreateTransactionStripe(s.Id, model);
        return Results.Json(new { redirect = s.Url, token = accessToken, tr = trStripe });
    }

    public static async Task<IResult> ConfirmationStripePayment([FromQuery] string session_id,
        ITransactionService srvcTransaction)
    {
        var srvc = new SessionService();
        Session s = srvc.Get(session_id);

        if (s.PaymentStatus == "paid")
        {
            await srvcTransaction.ConfirmTransactionStripe(s.Id);
            return Results.Redirect("https://rankedready.com/orderSent");
        }

        return Results.Redirect("https://rankedready.com/shopLol");
    }
}
