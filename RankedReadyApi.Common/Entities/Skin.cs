namespace RankedReadyApi.Common.Entities;

public class Skin
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ChampionName { get; set; } = default!;
    public string Information { get; set; } = default!;
    public string ImagePath { get; set; } = default!;
    public Skin(string championName, string information, string imagePath)
    {
        ChampionName = championName;
        Information = information;
        ImagePath = imagePath;
    }
}
