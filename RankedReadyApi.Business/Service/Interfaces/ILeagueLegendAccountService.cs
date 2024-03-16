using RankedReadyApi.Common.DataTransferObjects.LeagueLegendAccount;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.LeagueLegend;
using RankedReadyApi.Common.Models.Sorting;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface ILeagueLegendAccountService : IGenericServiceAsync<LeagueLegendAccount, AccountFullDto>
{
    Task<IEnumerable<AccountFullDto>> GetAllLeagueLegendAccounts(SortingPanel panel);
    Task<IEnumerable<AccountFullDto>> GetLeagueLegendAccounts(SortingPanel panel);
    Task<int> GetCountAvailableAccs();
    Task CreateLeagueLegendAcccount(LeagueLegendAccountModel model);
    Task UpdateLeagueLegendAcccount(LeagueLegendAccountModel model, Guid objectId);
    Task DeleteLeagueLegendAcccount(Guid objectId);
    Task UpdateStateInShop(bool isActive, Guid objectId);
    Task<IEnumerable<PurchaseAccountModel>> GetPurchasedAccounts(Guid userId);
    Task<AccountFullDto> GetFullAccountById(Guid accountId);
    Task<AccountWithoutCredDto> GetAccountWithoutCredById(Guid accountId);
    Task<AccountFullDto> GetNullableAccountById(Guid accountId);
}
