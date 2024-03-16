using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.DataTransferObjects.LeagueLegendAccount;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.LeagueLegend;
using RankedReadyApi.Common.Models.Sorting;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReadyApi.Business.Service.Implementations;

public class LeagueLegendAccountService :
    GenericServiceAsync<LeagueLegendAccount, AccountFullDto>,
    ILeagueLegendAccountService
{
    private readonly IServiceProvider _provider;

    private readonly ISkinService _srvcSkin;
    private readonly IUserService _srvcUser;

    public LeagueLegendAccountService(IMapper mapper, IUnitOfWork unitOfWork,
            ISkinService srvcSkin, IUserService srvcUser, IServiceProvider provider)
            : base(mapper, unitOfWork)
    {
        _srvcSkin = srvcSkin;
        _srvcUser = srvcUser;
        _provider = provider;
    }

    public async Task CreateLeagueLegendAcccount(LeagueLegendAccountModel model)
    {
        var skinId = Guid.Empty;
        if (model.SkinId != null)
        {
            var skin = await _srvcSkin.GetSkin(model.SkinId.Value);
            skinId = Guid.Parse(skin.Id);
        }

        var rank = model.Rank.ToEnum<RankLeagueLegend>();
        if (rank == RankLeagueLegend.None)
        {
            throw new ArgumentException("Rank doesnt exist");
        }

        var accLeagueLegend = new LeagueLegendAccount(rank, model.Login, model.Password,
            model.Region, model.AmountOfBe, model.Price, model.IsActiveInShop, model.EmailLogin, model.EmailPassword);

        if (skinId != Guid.Empty)
            accLeagueLegend.SkinId = skinId;
        await AddAsync(accLeagueLegend);
    }

    public async Task DeleteLeagueLegendAcccount(Guid objectId)
    {
        await DeleteAsync(objectId);
    }

    public async Task<AccountFullDto> GetFullAccountById(Guid accountId)
    {
        var account = await GetAsync(accountId);
        return account;
    }

    public async Task<AccountWithoutCredDto> GetAccountWithoutCredById(Guid accountId)
    {
        var account = await GetAsync(accountId);
        return mapper.Map<AccountFullDto, AccountWithoutCredDto>(account);
    }

    public async Task<IEnumerable<AccountFullDto>> GetAllLeagueLegendAccounts(SortingPanel panel)
    {
        var accounts = await GetAllByExpressionAsync(i => i.StateAccount != StateAccount.Soled);

        if (panel.Regions.Count != 0)
            accounts = accounts.Where(i => panel.Regions.Contains(i.Region));
        if (panel.MinPrice != null)
            accounts = accounts.Where(i => i.Price >= panel.MinPrice.Value);
        if (panel.MaxPrice != null)
            accounts = accounts.Where(i => i.Price <= panel.MaxPrice.Value);
        if (panel.MinAmountsOfBe != null)
            accounts = accounts.Where(i => i.AmountOfBe >= panel.MinAmountsOfBe.Value);
        if (panel.MaxAmountsOfBe != null)
            accounts = accounts.Where(i => i.AmountOfBe <= panel.MaxAmountsOfBe.Value);
        if (panel.SkinId != null)
            accounts = accounts.Where(i => i.SkinId == panel.SkinId.ToString());

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
            var enums = panel.Rank.ToEnums<RankLeagueLegend>().Select(i => i.ToString());
            accounts = accounts.Where(i => enums.Contains(i.Rank));
        }

        return accounts;
    }

    public async Task<int> GetCountAvailableAccs()
    {
        var accountsAvailable = await GetAllByExpressionAsync(i => i.IsActiveInShop && i.StateAccount == StateAccount.Active);
        return accountsAvailable.Count();
    }

    public async Task<IEnumerable<AccountFullDto>> GetLeagueLegendAccounts(SortingPanel panel)
    {
        var accounts = await GetAllByExpressionAsync(i => i.IsActiveInShop && i.StateAccount == StateAccount.Active);

        if (panel.Regions.Count != 0)
            accounts = accounts.Where(i => panel.Regions.Contains(i.Region));
        if (panel.MinPrice != null)
            accounts = accounts.Where(i => i.Price >= panel.MinPrice.Value);
        if (panel.MaxPrice != null)
            accounts = accounts.Where(i => i.Price <= panel.MaxPrice.Value);
        if (panel.MinAmountsOfBe != null)
            accounts = accounts.Where(i => i.AmountOfBe >= panel.MinAmountsOfBe.Value);
        if (panel.MaxAmountsOfBe != null)
            accounts = accounts.Where(i => i.AmountOfBe <= panel.MaxAmountsOfBe.Value);
        if (panel.SkinId != null)
            accounts = accounts.Where(i => i.SkinId == panel.SkinId.ToString());

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
            var enums = panel.Rank.ToEnums<RankLeagueLegend>().Select(i => i.ToString());
            accounts = accounts.Where(i => enums.Contains(i.Rank));
        }

        return accounts;
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
                Text = "Purchase a League Legend Account",
                Type = "LeagueLegendAccount"
            });
        }
        return purchasedAccounts;
    }

    public async Task UpdateLeagueLegendAcccount(LeagueLegendAccountModel model, Guid objectId)
    {
        var accounts = await GetAllByExpressionAsync(i => i.Id == objectId);

        var skinId = Guid.Empty;
        if (model.SkinId != null)
        {
            var skin = await _srvcSkin.GetSkin(model.SkinId.Value);
            skinId = Guid.Parse(skin.Id);
        }

        var rank = model.Rank.ToEnum<RankLeagueLegend>();
        if (rank == RankLeagueLegend.None)
        {
            throw new ArgumentException("Rank doesnt exist");
        }

        var accLeagueLegend = new LeagueLegendAccount(rank, model.Login, model.Password,
            model.Region, model.AmountOfBe, model.Price, model.IsActiveInShop, model.EmailLogin, model.EmailPassword);

        if (skinId != Guid.Empty)
            accLeagueLegend.SkinId = skinId;

        accLeagueLegend.Id = objectId;

        await UpdateAsync(mapper.Map<LeagueLegendAccount, AccountFullDto>(accLeagueLegend));
    }

    public async Task UpdateStateInShop(bool isActive, Guid objectId)
    {
        var account = await GetAsync(objectId);
        account.IsActiveInShop = isActive;
        await UpdateAsync(account);
    }
}
