using RankedReadyApi.Common.DataTransferObjects.Skin;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.Skin;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface ISkinService : IGenericServiceAsync<Skin, SkinDto>
{
    Task CreateSkin(SkinModel skin, string fileSrc);
    Task<SkinDto> GetSkin(Guid Id);
    Task DeleteSkin(Guid Id);
    Task<IEnumerable<SkinDto>> GetSkins();
}
