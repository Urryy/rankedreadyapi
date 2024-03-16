using AutoMapper;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.DataTransferObjects.Transaction;
using RankedReadyApi.Common.Entities;

namespace RankedReadyApi.Business.Service.Implementations;

public class TransactionStripeService : GenericServiceAsync<TransactionStripe, TransactionStripeDto>, ITransactionStripeService
{
    public TransactionStripeService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }
}
