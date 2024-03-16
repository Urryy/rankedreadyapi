using RankedReadyApi.Common.DataTransferObjects.ValorantAccount;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.LeagueLegend;
using RankedReadyApi.Common.Models.Sorting;
using RankedReadyApi.Common.Models.Valorant;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface IValorantAccountService : IGenericServiceAsync<ValorantAccount, AccountFullDto>
{
    Task<int> GetCountAvailableAccs();
    Task<IEnumerable<AccountFullDto>> GetAllValorantAccounts(SortingPanel panel);
    Task<IEnumerable<AccountFullDto>> GetValorantAccounts(SortingPanel panel);
    Task CreateValorantAcccount(ValorantAccountModel model);
    Task UpdateValorantAcccount(ValorantAccountModel model, Guid objectId);
    Task DeleteValorantAcccount(Guid objectId);
    Task UpdateStateInShop(bool isActive, Guid objectId);
    Task<AccountFullDto> GetFullAccountById(Guid accountId);
    Task<AccountWithoutCredDto> GetAccountWithoutCredById(Guid accountId);
    Task<IEnumerable<PurchaseAccountModel>> GetPurchasedAccounts(Guid userId);
    Task<AccountFullDto> GetNullableAccountById(Guid accountId);
}
