using AutoMapper;
using Microsoft.Extensions.Hosting;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.DataTransferObjects.Announcement;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.Announcement;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReadyApi.Business.Service.Implementations;

public class AnnouncementService : GenericServiceAsync<Announcement, AnnouncementDto>, IAnnouncementService
{
    private readonly IUserService _srvcUser;
    private readonly IHostEnvironment env;
    public AnnouncementService(IMapper mapper, IUnitOfWork unitOfWork,
            IUserService srvcUser, IHostEnvironment env)
        : base(mapper, unitOfWork)
    {
        _srvcUser = srvcUser;
        this.env = env;
    }

    public async Task CreateAnnouncement(AnnouncementModel model)
    {
        var announcementType = model.AnnouncementType.ToEnum<AnnouncementType>();
        var announcement = new Announcement(model.Heading, model.SubTitle, announcementType);
        await AddAsync(announcement);
    }

    public async Task DeleteAnnouncement(Guid objectId)
    {
        await DeleteAsync(objectId);
    }

    public async Task<AnnouncementDto> GetAnnouncement(Guid objectId)
    {
        return await GetAsync(objectId);
    }

    public async Task<IEnumerable<AnnouncementDto>> GetAnnouncements()
    {
        return await GetAllAsync();
    }

    public async Task UpdateAnnouncement(AnnouncementModel model, Guid objectId)
    {
        var announcement = await GetAsync(objectId);
        announcement.Heading = model.Heading;
        announcement.SubTitle = model.SubTitle;
        announcement.AnnouncementType = model.AnnouncementType;
        await UpdateAsync(announcement);
    }
}
