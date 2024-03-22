using RankedReadyApi.Configure;
using RankedReadyApi.CrossCutting.IoC.BuilderApplication;
using RankedReadyApi.CrossCutting.IoC.InversionDependency;
using RankedReadyApi.Extension;

var builder = WebApplication.CreateBuilder(args)
                    .ConfigureBuilder()
                    .ConnectDataAccess()
                    .InjectCommonAuthorization()
                    .ConnectBusiness();

builder.Build()
    .RegisterEndpoints()
    .ConfigureApplication()
    .Run();

