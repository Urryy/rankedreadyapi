using RankedReadyApi.Common.DataTransferObjects.Transaction;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.Transaction;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface ITransactionService : IGenericServiceAsync<Transaction, TransactionDto>
{
    Task<IEnumerable<TransactionCommonDto>> GetAllTransactions();

    Task<byte[]> ExportToExcel();

    Task<TransactionCommonDto> GetTransactionDetails(Guid objectId);

    Task<TransactionDto> CreateTransaction(TransactionModel model);

    Task DeleteTransaction(Guid transactionId);

    Task<bool> ReturnCallbackTransaction(Transaction_Result result);

    Task<Transaction_Response> Pay(Guid Id);

    Task<int> GetCountSuccededTransactions();

    Task<ProfitModel> GetProfit();

    Task<IEnumerable<TransactionCommonDto>> GetUserTransactions(Guid userId);

    Task<TransactionStripeDto> CreateTransactionStripe(string id, TransactionModel model);

    Task ConfirmTransactionStripe(string sessionId);

    Task<DatePurchaseModel> GetDateTransactionByAccountId(Guid accountId);
}
