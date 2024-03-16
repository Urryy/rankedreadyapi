using RankedReadyApi.CrossCutting.IoC.BuilderApplication;
using RankedReadyApi.CrossCutting.IoC.InversionDependency;
using RankedReadyApi.Extension;

var builder = WebApplication.CreateBuilder(args);

var str = $"/skins/{{objectId:Guid}}";
builder.Services.ConnectDataAccess(builder.Configuration)
                .ConnectBusiness(builder.Configuration)
                .InjectCommonAuthorization(builder.Configuration);

builder.Build()
       .RegisterEndpoints()
       .ConfigureApplication()
       .Run();
