using RankedReadyApi.Common.DataTransferObjects.Transaction;
using RankedReadyApi.Common.Entities;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface ITransactionStripeService : IGenericServiceAsync<TransactionStripe, TransactionStripeDto>
{
}
