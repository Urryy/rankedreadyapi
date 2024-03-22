using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RankedReady.DataAccess.Mapping;
using RankedReady.DataAccess.Repository.Implementations;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReady.DataAccess.Validators.Announcement;
using RankedReady.DataAccess.Validators.LeagueLegend;
using RankedReady.DataAccess.Validators.Skin;
using RankedReady.DataAccess.Validators.Statistic;
using RankedReady.DataAccess.Validators.SupportTicket;
using RankedReady.DataAccess.Validators.Token;
using RankedReady.DataAccess.Validators.Transaction;
using RankedReady.DataAccess.Validators.User;
using RankedReady.DataAccess.Validators.Valorant;
using RankedReadyApi.Common.Context;
using RankedReadyApi.Common.Models.Announcement;
using RankedReadyApi.Common.Models.LeagueLegend;
using RankedReadyApi.Common.Models.Skin;
using RankedReadyApi.Common.Models.Statistic;
using RankedReadyApi.Common.Models.SupportTicket;
using RankedReadyApi.Common.Models.Token;
using RankedReadyApi.Common.Models.Transaction;
using RankedReadyApi.Common.Models.User;
using RankedReadyApi.Common.Models.Valorant;

namespace RankedReadyApi.CrossCutting.IoC.InversionDependency;

public static class InjectDataAccess
{
    public static WebApplicationBuilder ConnectDataAccess(this WebApplicationBuilder builder)
    {
        InjectDataBase(builder.Services, builder.Configuration);
        InjectAutomapper(builder.Services);
        InjectRepositories(builder.Services);
        InjectValidators(builder.Services);
        return builder;
    }
    private static void InjectDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDataBaseContext>();
        var sqlConnect = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDataBaseContext>(options => options.UseSqlServer(sqlConnect));
    }

    private static void InjectAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
    }

    private static void InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
    }

    private static void InjectValidators(this IServiceCollection services)
    {
        #region Announcement Validator
        services.AddScoped<IValidator<AnnouncementModel>, AnnouncementValidator.AnnouncementModelValidator>();
        #endregion

        #region User Validator
        services.AddScoped<IValidator<RegisterUserModel>, UserValidator.RegisterUserValidator>();
        services.AddScoped<IValidator<LoginUserModel>, UserValidator.LoginUserValidator>();
        #endregion

        #region SupportTicket Validator
        services.AddScoped<IValidator<SupportTicketModel>, SupportTicketValidator.SupportTicketModelValidator>();
        services.AddScoped<IValidator<AskModel>, SupportTicketValidator.AskModelValidator>();
        #endregion

        #region League Legend Validator
        services.AddScoped<IValidator<LeagueLegendAccountModel>, LeagueLegendValidator.LeagueLegendModelValidator>();
        #endregion

        #region Valorant Validator
        services.AddScoped<IValidator<ValorantAccountModel>, ValorantValidator.ValorantModelValidator>();
        #endregion

        #region Skin Validator
        services.AddScoped<IValidator<SkinModel>, SkinValidator.SkinModelValidator>();
        #endregion

        #region Statistic Validator
        services.AddScoped<IValidator<PeriodSortingModel>, StatisticValidator.PeriodSortingModelValidator>();
        #endregion

        #region Token Validator
        services.AddScoped<IValidator<TokenRefreshModel>, TokenValidator.TokenRefreshModelValidator>();
        #endregion

        #region
        services.AddScoped<IValidator<TransactionModel>, TransactionValidator.TransactionModelValidator>();
        #endregion
    }
}
