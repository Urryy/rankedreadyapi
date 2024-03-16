using AutoMapper;
using ClosedXML.Excel;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration;
using RankedReady.DataAccess.Extensions;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Cosntants.EmailBody;
using RankedReadyApi.Common.DataTransferObjects.Transaction;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.Transaction;
using System.Data;
using System.Globalization;

namespace RankedReadyApi.Business.Service.Implementations;

public class TransactionService : GenericServiceAsync<Transaction, TransactionDto>, ITransactionService
{
    private readonly ITransactionStripeService _srvcTransactionStripe;
    private readonly IUserService _srvcUser;
    private readonly ILeagueLegendAccountService _srvcLeagueLegendAccount;
    private readonly IValorantAccountService _srvcValorantAccount;
    private readonly IFluentEmail _fluentEmail;
    private readonly IConfiguration _configuration;

    public TransactionService(IMapper mapper, IUnitOfWork unitOfWork,
        ITransactionStripeService srvcTransactionStripe, IUserService srvcUser,
        ILeagueLegendAccountService srvcLeagueLegendAccount, IValorantAccountService srvcValorantAccount,
        IConfiguration configuration, IFluentEmail fluentEmail) : base(mapper, unitOfWork)
    {
        _srvcTransactionStripe = srvcTransactionStripe;
        _srvcUser = srvcUser;
        _srvcLeagueLegendAccount = srvcLeagueLegendAccount;
        _srvcValorantAccount = srvcValorantAccount;
        _fluentEmail = fluentEmail;
        _configuration = configuration;
    }

    public async Task DeleteTransaction(Guid transactionId)
    {
        await DeleteAsync(transactionId);
        await _srvcTransactionStripe.DeleteAsync(transactionId);
    }

    public async Task<byte[]> ExportToExcel()
    {
        var commonTransactionsDto = new List<TransactionCommonDto>();

        var transactionsPaytabs = await GetAllAsync();
        foreach (var item in transactionsPaytabs)
            commonTransactionsDto.Add(TransactionCommonDto.CreateDto(item));

        var transactionsStripe = await GetAllAsync();
        foreach (var item in transactionsStripe)
            commonTransactionsDto.Add(TransactionCommonDto.CreateDto(item));

        DataTable dt = new DataTable();
        dt.TableName = "Transactions";
        dt.Columns.Add("id");
        dt.Columns.Add("AccountId");
        dt.Columns.Add("UserId");
        dt.Columns.Add("Email");
        dt.Columns.Add("Amount");
        dt.Columns.Add("CreatedDate");
        dt.Columns.Add("Status");

        foreach (var item in commonTransactionsDto)
        {
            dt.Rows.Add(item.Id, item.AccountId, item.UserId, item.Email, item.Amount, item.CreatedDate, item.Status);
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            var sheet = wb.AddWorksheet(dt, "Transactions");

            using (MemoryStream ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                return ms.ToArray();
            }
        }
    }

    public async Task<IEnumerable<TransactionCommonDto>> GetAllTransactions()
    {
        var commonTransactionsDto = new List<TransactionCommonDto>();

        var transactionsPaytabs = await GetAllAsync();
        foreach (var item in transactionsPaytabs)
            commonTransactionsDto.Add(TransactionCommonDto.CreateDto(item));

        var transactionsStripe = await GetAllAsync();
        foreach (var item in transactionsStripe)
            commonTransactionsDto.Add(TransactionCommonDto.CreateDto(item));

        return commonTransactionsDto;
    }

    public async Task<int> GetCountSuccededTransactions()
    {
        return await Count();
    }

    public async Task<ProfitModel> GetProfit()
    {
        var profitModel = new ProfitModel();

        var transactionPaytabs = await GetAllByExpressionAsync(i => i.IsSucceed != null && i.IsSucceed.Value == true);
        var transactionStripe = await _srvcTransactionStripe.GetAllByExpressionAsync(i => i.IsCompleted);

        var totalCryptoProfit = transactionPaytabs.Select(i => i.CartAmount).Sum();
        var totalStripeProfit = transactionStripe.Select(i => i.Amount).Sum();

        profitModel.TotalProfit = (totalCryptoProfit + totalStripeProfit).ToString();

        var groupBySuccededPaytabsTransactions = transactionPaytabs.GroupBy(i => new { i.DateTransaction, i.CartAmount });
        foreach (var item in groupBySuccededPaytabsTransactions)
        {
            profitModel.ProfitPerDay.Add(item.Key.DateTransaction.ToString(), item.Key.CartAmount);
        }

        var groupBySuccededStripeTransactions = transactionStripe.GroupBy(i => new { i.CreatedDate, i.Amount });
        foreach (var item in groupBySuccededStripeTransactions)
        {
            profitModel.ProfitPerDay.Add(item.Key.CreatedDate.ToString(), (float)item.Key.Amount);
        }

        var sortedDict = profitModel.ProfitPerDay.OrderBy(x => x.Value);
        if (sortedDict.Count() > 0)
        {
            profitModel.MinProfitPerDay = sortedDict.First().Value.ToString();
            profitModel.MaxProfitPerDay = sortedDict.Last().Value.ToString();
        }

        return profitModel;
    }

    public async Task<TransactionCommonDto> GetTransactionDetails(Guid objectId)
    {
        var transactionPaytabs = await GetOrDefaultAsync(objectId);
        var transactionStripe = await _srvcTransactionStripe.GetOrDefaultAsync(objectId);
        if (transactionPaytabs != null)
            return TransactionCommonDto.CreateDto(transactionPaytabs);
        else if (transactionStripe != null)
            return TransactionCommonDto.CreateDto(transactionStripe);
        else
            throw new NullReferenceException($"Transaction doesnt exist with this id : \"{objectId}\"");
    }

    public async Task<IEnumerable<TransactionCommonDto>> GetUserTransactions(Guid userId)
    {
        var user = await _srvcUser.GetAsync(userId);

        var commonTransactionsDto = new List<TransactionCommonDto>();

        var transactionsPaytabs = await GetAllByExpressionAsync(i => i.UserId == userId);
        foreach (var item in transactionsPaytabs)
            commonTransactionsDto.Add(TransactionCommonDto.CreateDto(item));

        var transactionsStripe = await GetAllByExpressionAsync(i => i.UserId == userId);
        foreach (var item in transactionsStripe)
            commonTransactionsDto.Add(TransactionCommonDto.CreateDto(item));

        return commonTransactionsDto;
    }

    public async Task<DatePurchaseModel> GetDateTransactionByAccountId(Guid accountId)
    {
        var transaction = await GetOrDefaultAsync(accountId);

        if (transaction != null)
        {
            return transaction.DateTransaction.GetPurchaseModel();
        }

        var transactionStripe = await _srvcTransactionStripe.GetOrDefaultAsync(accountId);
        if (transactionStripe != null)
        {
            return transactionStripe.CreatedDate.GetPurchaseModel();
        }

        throw new ArgumentException($"Doesn't exist account by this ID : {accountId}");
    }

    #region Stripe payment
    public async Task<TransactionStripeDto> CreateTransactionStripe(string id, TransactionModel model)
    {
        var user = await _srvcUser.GetAsync(model.UserId!.Value);

        var leagueAccount = await _srvcLeagueLegendAccount.GetOrDefaultAsync(model.AccountId);
        var valoranAccount = await _srvcValorantAccount.GetOrDefaultAsync(model.AccountId);

        if (valoranAccount == null && leagueAccount == null)
        {
            throw new ArgumentException("Account doesn't exist");
        }

        var price = valoranAccount != null
            ? float.Parse(valoranAccount.Price.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture)
            : float.Parse(leagueAccount.Price.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture);

        var trStripe = new TransactionStripe(model.UserId.Value, model.AccountId, price, id, false, model.Email ?? user.Email);
        await _srvcTransactionStripe.AddAsync(trStripe);

        return await _srvcTransactionStripe.GetAsync(trStripe.Id);
    }

    public async Task ConfirmTransactionStripe(string sessionId)
    {
        var transaction = await _srvcTransactionStripe.GetByExpressionAsync(i => i.SessionId == sessionId);
        transaction.IsCompleted = true;
        await _srvcTransactionStripe.UpdateAsync(transaction);

        var email = string.Empty;
        var emailPassword = string.Empty;
        var loginAccount = string.Empty;
        var passwordAccount = string.Empty;

        var leagueAccount = await _srvcLeagueLegendAccount.GetOrDefaultAsync(Guid.Parse(transaction.AccountId));
        var valoranAccount = await _srvcValorantAccount.GetOrDefaultAsync(Guid.Parse(transaction.AccountId));

        if (leagueAccount == null)
        {
            email = valoranAccount.EmailLogin;
            emailPassword = valoranAccount.EmailPassword;
            loginAccount = valoranAccount.Login;
            passwordAccount = valoranAccount.Password;

            valoranAccount.UserId = transaction.UserId;
            valoranAccount.StateAccount = StateAccount.Soled.ToString();
            valoranAccount.IsActiveInShop = false;
            await _srvcValorantAccount.UpdateAsync(valoranAccount);

            var emailForSend = _fluentEmail.To(transaction.Email)
            .Subject("RANKED READY")
            .Body(string.Format(EmailBodyConsts.EmailBodyMessageValorant, email, emailPassword, loginAccount, passwordAccount), true);

            var sendResponse = await emailForSend.SendAsync();
            if (!sendResponse.Successful)
            {
                throw new ArgumentException("email didn't send" + sendResponse.ErrorMessages.FirstOrDefault());
            }
        }
        else
        {
            email = leagueAccount.EmailLogin;
            emailPassword = leagueAccount.EmailPassword;
            loginAccount = leagueAccount.Login;
            passwordAccount = leagueAccount.Password;

            leagueAccount.UserId = transaction.UserId;
            leagueAccount.StateAccount = StateAccount.Soled.ToString();
            leagueAccount.IsActiveInShop = false;
            await _srvcLeagueLegendAccount.UpdateAsync(leagueAccount);

            var emailForSend = _fluentEmail.To(transaction.Email)
            .Subject("RANKED READY")
            .Body(string.Format(EmailBodyConsts.EmailBodyMessageLeague, email, emailPassword, loginAccount, passwordAccount), true);

            var sendResponse = await emailForSend.SendAsync();
            if (!sendResponse.Successful)
            {
                throw new ArgumentException("email didn't send" + sendResponse.ErrorMessages.FirstOrDefault());
            }
        }
    }
    #endregion

    #region Paytabs payment
    public async Task<TransactionDto> CreateTransaction(TransactionModel model)
    {
        try
        {
            var user = await _srvcUser.GetAsync(model.UserId!.Value);

            var leagueAccount = await _srvcLeagueLegendAccount.GetOrDefaultAsync(model.AccountId);
            var valoranAccount = await _srvcValorantAccount.GetOrDefaultAsync(model.AccountId);

            if (valoranAccount == null && leagueAccount == null)
            {
                throw new ArgumentException("Account doesn't exist");
            }

            var price = valoranAccount != null
                ? float.Parse(valoranAccount.Price.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture)
                : float.Parse(leagueAccount.Price.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture);

            var tr = new Transaction(Guid.Parse(user.Id), model.AccountId, model.Email ?? user.Email, null, null, null, null, 134779,
                _configuration["Paytabs:ApiServerKey"]!, "https://secure-global.paytabs.com/", "sale", "ecom", Guid.NewGuid().ToString(), "USD",
                price, $"{model.AccountId} - {model.UserId} - {model.Email}", "en", true, true, model.returnUrl!, model.callbackUrl!);

            await AddAsync(tr);
            return await GetAsync(tr.Id);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the transaction");
        }
    }

    public async Task<Transaction_Response> Pay(Guid Id)
    {
        var transaction = await GetAsync(Id);
        TransactionConnector c = new TransactionConnector();
        Transaction_Response r = c.Send(TransactionTransfer.GetTransactionTransfer(transaction));

        if (r.IsSuccess())
        {
            transaction.Tran_Ref = r.tran_ref;
            transaction.TriedToPay = true;

            await UpdateAsync(transaction);

            return r;
        }
        return null;
    }

    public async Task<bool> ReturnCallbackTransaction(Transaction_Result result)
    {
        var transaction = await GetByExpressionAsync(i => i.CartId == result.cartId);

        bool valid = result.IsValid_Signature(transaction.ServerKey);

        transaction.IsValid_Signature = valid;
        transaction.IsSucceed = result.IsSucceed();
        if (transaction.IsSucceed.Value)
        {
            await UpdateAsync(transaction);

            var email = string.Empty;
            var emailPassword = string.Empty;
            var loginAccount = string.Empty;
            var passwordAccount = string.Empty;

            var leagueAccount = await _srvcLeagueLegendAccount.GetOrDefaultAsync(Guid.Parse(transaction.AccountId));
            var valoranAccount = await _srvcValorantAccount.GetOrDefaultAsync(Guid.Parse(transaction.AccountId));

            if (leagueAccount == null)
            {
                email = valoranAccount.EmailLogin;
                emailPassword = valoranAccount.EmailPassword;
                loginAccount = valoranAccount.Login;
                passwordAccount = valoranAccount.Password;

                valoranAccount.UserId = transaction.UserId;
                valoranAccount.StateAccount = StateAccount.Soled.ToString();
                valoranAccount.IsActiveInShop = false;
                await _srvcValorantAccount.UpdateAsync(valoranAccount);

                var emailForSend = _fluentEmail.To(transaction.Email)
                .Subject("RANKED READY")
                .Body(string.Format(EmailBodyConsts.EmailBodyMessageValorant, email, emailPassword, loginAccount, passwordAccount), true);

                var sendResponse = await emailForSend.SendAsync();
                if (!sendResponse.Successful)
                {
                    throw new ArgumentException("email didn't send" + sendResponse.ErrorMessages.FirstOrDefault());
                }
            }
            else
            {
                email = leagueAccount.EmailLogin;
                emailPassword = leagueAccount.EmailPassword;
                loginAccount = leagueAccount.Login;
                passwordAccount = leagueAccount.Password;

                leagueAccount.UserId = transaction.UserId;
                leagueAccount.StateAccount = StateAccount.Soled.ToString();
                leagueAccount.IsActiveInShop = false;
                await _srvcLeagueLegendAccount.UpdateAsync(leagueAccount);

                var emailForSend = _fluentEmail.To(transaction.Email)
                .Subject("RANKED READY")
                .Body(string.Format(EmailBodyConsts.EmailBodyMessageLeague, email, emailPassword, loginAccount, passwordAccount), true);

                var sendResponse = await emailForSend.SendAsync();
                if (!sendResponse.Successful)
                {
                    throw new ArgumentException("email didn't send" + sendResponse.ErrorMessages.FirstOrDefault());
                }
            }
        }
        return valid;
    }
    #endregion
}
