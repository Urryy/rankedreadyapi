using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.DataTransferObjects.ValorantAccount;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.LeagueLegend;
using RankedReadyApi.Common.Models.Sorting;
using RankedReadyApi.Common.Models.Valorant;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReadyApi.Business.Service.Implementations;

public class ValorantAccountService :
    GenericServiceAsync<ValorantAccount, AccountFullDto>,
    IValorantAccountService
{
    private readonly IServiceProvider _provider;

    private readonly ISkinService _srvcSkin;
    private readonly IUserService _srvcUser;

    public ValorantAccountService(IMapper mapper, IUnitOfWork unitOfWork,
            ISkinService srvcSkin, IUserService srvcUser, IServiceProvider provider)
            : base(mapper, unitOfWork)
    {
        _srvcSkin = srvcSkin;
        _srvcUser = srvcUser;
        _provider = provider;
    }

    public async Task CreateValorantAcccount(ValorantAccountModel model)
    {
        var skinId = Guid.Empty;
        if (model.SkinId != null)
        {
            var skin = await _srvcSkin.GetSkin(model.SkinId.Value);
            skinId = Guid.Parse(skin.Id);
        }

        var rank = model.Rank.ToEnum<RankValorant>();
        if (rank == RankValorant.None)
        {
            throw new ArgumentException("Rank doesnt exist");
        }

        var accValorant = new ValorantAccount(rank, model.Login, model.Password,
            model.Region, model.Price, model.IsActiveInShop, model.EmailLogin, model.EmailPassword);

        if (skinId != Guid.Empty)
            accValorant.SkinId = skinId;
        await AddAsync(accValorant);
    }

    public async Task DeleteValorantAcccount(Guid objectId)
    {
        await DeleteAsync(objectId);
    }

    public async Task<AccountFullDto> GetFullAccountById(Guid accountId)
    {
        return await GetAsync(accountId);
    }

    public async Task<AccountWithoutCredDto> GetAccountWithoutCredById(Guid accountId)
    {
        var account = await GetAsync(accountId);
        return mapper.Map<AccountFullDto, AccountWithoutCredDto>(account);
    }

    public async Task<IEnumerable<AccountFullDto>> GetAllValorantAccounts(SortingPanel panel)
    {
        var accounts = await GetAllByExpressionAsync(i => i.StateAccount != StateAccount.Soled);

        if (panel.Regions.Count != 0)
            accounts = accounts.Where(i => panel.Regions.Contains(i.Region));
        if (panel.MinPrice != null)
            accounts = accounts.Where(i => i.Price >= panel.MinPrice.Value);
        if (panel.MaxPrice != null)
            accounts = accounts.Where(i => i.Price <= panel.MaxPrice.Value);

        if (!string.IsNullOrEmpty(panel.SortingType))
        {
            var rank = panel.SortingType.ToEnum<SortingType>();
            switch (rank)
            {
                case SortingType.Rank:
                    accounts = accounts.OrderBy(i => i.Rank);
                    break;
                case SortingType.PriceMin:
                    accounts = accounts.OrderBy(i => i.Price);
                    break;
                case SortingType.PriceMax:
                    accounts = accounts.OrderByDescending(i => i.Price);
                    break;
                default:
                    break;
            }
        }

        if (panel.Rank.Count > 0)
        {
            var enums = panel.Rank.ToEnums<RankValorant>().Select(i => i.ToString());
            accounts = accounts.Where(i => enums.Contains(i.Rank));
        }

        return accounts;
    }

    public async Task<int> GetCountAvailableAccs()
    {
        var accountsAvailable = await GetAllByExpressionAsync(i => i.IsActiveInShop && i.StateAccount == StateAccount.Active);
        return accountsAvailable.Count();
    }

    public async Task<AccountFullDto> GetNullableAccountById(Guid accountId)
    {
        var accounts = await GetAllAsync();
        var account = accounts.FirstOrDefault(i => i.Id == accountId.ToString());
        return account;
    }

    public async Task<IEnumerable<PurchaseAccountModel>> GetPurchasedAccounts(Guid userId)
    {
        var user = await _srvcUser.GetAsync(userId);
        var accounts = await GetAllByExpressionAsync(i => i.UserId != null && i.UserId.Equals(userId)
                && i.StateAccount == StateAccount.Soled);

        var transactionService = _provider.GetRequiredService<ITransactionService>();
        var purchasedAccounts = new List<PurchaseAccountModel>();
        foreach (var item in accounts)
        {
            var purchaseDate = await transactionService.GetDateTransactionByAccountId(Guid.Parse(item.Id));
            purchasedAccounts.Add(new PurchaseAccountModel
            {
                Id = item.Id.ToString(),
                Rank = item.Rank.ToString(),
                Region = item.Region,
                Price = item.Price,
                IsActiveInShop = item.IsActiveInShop,
                DatePurchase = purchaseDate.Date,
                MonthName = purchaseDate.MonthName,
                DayOfMonth = purchaseDate.DayOfMonth,
                Time = purchaseDate.Time,
                Text = "Purchase a Valorant Account",
                Type = "ValorantAccount"
            });
        }
        return purchasedAccounts;
    }

    public async Task<IEnumerable<AccountFullDto>> GetValorantAccounts(SortingPanel panel)
    {
        var accounts = await GetAllByExpressionAsync(i => i.IsActiveInShop && i.StateAccount == StateAccount.Active);

        if (panel.Regions.Count != 0)
            accounts = accounts.Where(i => panel.Regions.Contains(i.Region));
        if (panel.MinPrice != null)
            accounts = accounts.Where(i => i.Price >= panel.MinPrice.Value);
        if (panel.MaxPrice != null)
            accounts = accounts.Where(i => i.Price <= panel.MaxPrice.Value);

        if (!string.IsNullOrEmpty(panel.SortingType))
        {
            var rank = panel.SortingType.ToEnum<SortingType>();
            switch (rank)
            {
                case SortingType.Rank:
                    accounts = accounts.OrderBy(i => i.Rank);
                    break;
                case SortingType.PriceMin:
                    accounts = accounts.OrderBy(i => i.Price);
                    break;
                case SortingType.PriceMax:
                    accounts = accounts.OrderByDescending(i => i.Price);
                    break;
                default:
                    break;
            }
        }

        if (panel.Rank.Count > 0)
        {
            var enums = panel.Rank.ToEnums<RankValorant>().Select(i => i.ToString());
            accounts = accounts.Where(i => enums.Contains(i.Rank));
        }

        return accounts;
    }

    public async Task UpdateStateInShop(bool isActive, Guid objectId)
    {
        var account = await GetAsync(objectId);
        account.IsActiveInShop = isActive;
        await UpdateAsync(account);
    }

    public async Task UpdateValorantAcccount(ValorantAccountModel model, Guid objectId)
    {
        var skinId = Guid.Empty;
        if (model.SkinId != null)
        {
            var skin = await _srvcSkin.GetSkin(model.SkinId.Value);
            skinId = Guid.Parse(skin.Id);
        }

        var rank = model.Rank.ToEnum<RankValorant>();
        if (rank == RankValorant.None)
        {
            throw new ArgumentException("Rank doesnt exist");
        }

        var accValorant = new ValorantAccount(rank, model.Login, model.Password,
            model.Region, model.Price, model.IsActiveInShop, model.EmailLogin, model.EmailPassword);

        if (skinId != Guid.Empty)
            accValorant.SkinId = skinId;

        accValorant.Id = objectId;
        await UpdateAsync(mapper.Map<ValorantAccount, AccountFullDto>(accValorant));
    }
}
