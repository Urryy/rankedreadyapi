using RankedReadyApi.Common.DataTransferObjects.Announcement;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.Announcement;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface IAnnouncementService : IGenericServiceAsync<Announcement, AnnouncementDto>
{
    Task CreateAnnouncement(AnnouncementModel model);
    Task UpdateAnnouncement(AnnouncementModel model, Guid objectId);
    Task<AnnouncementDto> GetAnnouncement(Guid objectId);
    Task DeleteAnnouncement(Guid objectId);
    Task<IEnumerable<AnnouncementDto>> GetAnnouncements();
}
