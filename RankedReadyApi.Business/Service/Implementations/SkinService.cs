using AutoMapper;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.DataTransferObjects.Skin;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.Skin;

namespace RankedReadyApi.Business.Service.Implementations;

public class SkinService : GenericServiceAsync<Skin, SkinDto>, ISkinService
{
    public SkinService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task CreateSkin(SkinModel skin, string fileSrc)
    {
        string pathSkins = Path.Combine(SystemService.GetApplicationFolder(), "SkinsImages");

        string filePath = Path.Combine(pathSkins, skin.Skin.FileName);
        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        {
            await skin.Skin.CopyToAsync(fileStream);
        }

        var skinDb = new Skin(skin.ChampionName, skin.Information, fileSrc);
        await AddAsync(skinDb);
    }

    public async Task DeleteSkin(Guid Id)
    {
        await DeleteAsync(Id);
    }

    public async Task<SkinDto> GetSkin(Guid Id)
    {
        return await GetAsync(Id);
    }

    public async Task<IEnumerable<SkinDto>> GetSkins()
    {
        return await GetAllAsync();
    }
}
