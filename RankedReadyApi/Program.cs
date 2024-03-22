using RankedReadyApi.Configure;
using RankedReadyApi.CrossCutting.IoC.BuilderApplication;
using RankedReadyApi.CrossCutting.IoC.InversionDependency;
using RankedReadyApi.Extension;

WebApplication.CreateBuilder(args)
                .ConfigureBuilder()
                .ConnectDataAccess()
                .InjectCommonAuthorization()
                .ConnectBusiness()
                .Build()
                .RegisterEndpoints()
                .ConfigureApplication()
                .Run();

