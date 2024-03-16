using RankedReadyApi.Common.Enums;

namespace RankedReadyApi.Common.Entities;

public class Announcement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Heading { get; set; }
    public string SubTitle { get; set; }
    public AnnouncementType AnnouncementType { get; set; }

    public Announcement(string heading, string subTitle, AnnouncementType announcementType)
    {
        Heading = heading;
        SubTitle = subTitle;
        AnnouncementType = announcementType;
    }
}
