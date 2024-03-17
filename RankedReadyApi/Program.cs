using RankedReadyApi.CrossCutting.IoC.BuilderApplication;
using RankedReadyApi.CrossCutting.IoC.InversionDependency;
using RankedReadyApi.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConnectDataAccess(builder.Configuration)
                .InjectCommonAuthorization(builder.Configuration)
                .ConnectBusiness(builder.Configuration)
                .AddControllers();

var app = builder.Build();
app.RegisterEndpoints()
   .ConfigureApplication()
   .Run();

