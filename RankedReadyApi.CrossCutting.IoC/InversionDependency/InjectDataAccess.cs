using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RankedReady.DataAccess.Mapping;
using RankedReady.DataAccess.Repository.Implementations;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReady.DataAccess.Validators.Announcement;
using RankedReadyApi.Common.Context;
using RankedReadyApi.Common.Models.Announcement;

namespace RankedReadyApi.CrossCutting.IoC.InversionDependency;

public static class InjectDataAccess
{
    public static IServiceCollection ConnectDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        InjectDataBase(services, configuration);
        InjectAutomapper(services);
        InjectRepositories(services);
        InjectValidators(services);
        return services;
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
        services.AddScoped<IValidator<AnnouncementModel>, AnnouncementValidator>();
    }
}
