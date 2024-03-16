namespace RankedReadyApi.Common.Models.LeagueLegend;

public class PurchaseAccountModel
{
    public string Id { get; set; } = default!;
    public string SkinId { get; set; } = default!;
    public string Rank { get; set; } = default!;
    public string Region { get; set; } = default!;
    public double Price { get; set; } = default!;
    public bool IsActiveInShop { get; set; } = default!;
    public string DatePurchase { get; set; } = default!;
    public string MonthName { get; set; } = default!;
    public string DayOfMonth { get; set; } = default!;
    public string Time { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string Type { get; set; } = default!;
}
