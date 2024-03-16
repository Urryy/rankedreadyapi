using AutoMapper;
using RankedReadyApi.Common.DataTransferObjects.Announcement;
using RankedReadyApi.Common.DataTransferObjects.Code;
using RankedReadyApi.Common.DataTransferObjects.Skin;
using RankedReadyApi.Common.DataTransferObjects.SupportTicket;
using RankedReadyApi.Common.DataTransferObjects.Transaction;
using RankedReadyApi.Common.DataTransferObjects.User;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReady.DataAccess.Mapping;

using LeagueLegendAccountFullDto = RankedReadyApi.Common.DataTransferObjects.LeagueLegendAccount.AccountFullDto;
using LeagueLegendAccountWithoutCredDto = RankedReadyApi.Common.DataTransferObjects.LeagueLegendAccount.AccountWithoutCredDto;

using ValorantAccountFullDto = RankedReadyApi.Common.DataTransferObjects.ValorantAccount.AccountFullDto;
using ValorantAccountWithoutCredDto = RankedReadyApi.Common.DataTransferObjects.ValorantAccount.AccountWithoutCredDto;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Announcement Map
        CreateMap<Announcement, AnnouncementDto>()
            .ForMember(dest => dest.AnnouncementType, opt => opt.MapFrom(src => src.AnnouncementType.ToString()));
        CreateMap<AnnouncementDto, Announcement>()
            .ForMember(dest => dest.AnnouncementType, opt => opt.MapFrom(src => src.AnnouncementType.ToEnum<AnnouncementType>()));
        #endregion

        #region CodeChangedPassword Map
        CreateMap<CodeChangedPassword, CodeChangedPasswordDto>().ReverseMap();
        #endregion

        #region LeagueLegendAccount Map
        CreateMap<LeagueLegendAccount, LeagueLegendAccountFullDto>()
            .ForMember(dest => dest.StateAccount, opt => opt.MapFrom(src => src.StateAccount.ToString()))
            .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank.ToString()));

        CreateMap<LeagueLegendAccountFullDto, LeagueLegendAccount>()
            .ForMember(dest => dest.StateAccount, opt => opt.MapFrom(src => src.Rank.ToEnum<StateAccount>()))
            .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank.ToEnum<RankLeagueLegend>()));

        CreateMap<LeagueLegendAccountFullDto, LeagueLegendAccountWithoutCredDto>()
            .ReverseMap();
        #endregion

        #region Skin Map
        CreateMap<Skin, SkinDto>().ReverseMap();
        #endregion

        #region SupportTicket Map
        CreateMap<SupportTicket, SupportTicketDto>().ReverseMap();
        #endregion

        #region Transaction Map
        CreateMap<Transaction, TransactionDto>().ReverseMap();
        #endregion

        #region TransactionStripe Map
        CreateMap<TransactionStripe, TransactionStripeDto>().ReverseMap();
        #endregion

        #region User Map
        CreateMap<User, UserDto>().ReverseMap()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToString()));

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToEnum<StateAccount>()))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToEnum<RankLeagueLegend>()));

        CreateMap<UserDto, UserWithoutCredDto>()
            .ReverseMap();
        #endregion

        #region ValorantAccount Map
        CreateMap<ValorantAccount, ValorantAccountFullDto>()
            .ForMember(dest => dest.StateAccount, opt => opt.MapFrom(src => src.StateAccount.ToString()))
            .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank.ToString()));

        CreateMap<ValorantAccountFullDto, ValorantAccount>()
            .ForMember(dest => dest.StateAccount, opt => opt.MapFrom(src => src.Rank.ToEnum<StateAccount>()))
            .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank.ToEnum<RankValorant>()));

        CreateMap<ValorantAccountFullDto, ValorantAccountWithoutCredDto>()
            .ReverseMap();
        #endregion
    }
}
