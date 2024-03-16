namespace RankedReadyApi.Common.Models.Transaction;

public class ProfitModel
{
    public Dictionary<string, float> ProfitPerDay { get; set; } = new Dictionary<string, float>();
    public string TotalProfit { get; set; }
    public string MinProfitPerDay { get; set; }
    public string MaxProfitPerDay { get; set; }
}
