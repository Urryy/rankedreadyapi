using Microsoft.AspNetCore.Http;

namespace RankedReadyApi.Common.Models.Skin;

public class SkinModel
{
    public string ChampionName { get; set; } = default!;
    public string Information { get; set; } = default!;
    public IFormFile Skin { get; set; } = default!;
}
