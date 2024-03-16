namespace RankedReadyApi.Common.Models.Sorting;

public class SortingPanel
{
    public List<string> Regions { get; set; } = new List<string>();
    public string SortingType { get; set; } = default!;
    public int? MinAmountsOfBe { get; set; }
    public int? MaxAmountsOfBe { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public Guid? SkinId { get; set; }
    public List<string> Rank { get; set; } = new List<string>();
}
