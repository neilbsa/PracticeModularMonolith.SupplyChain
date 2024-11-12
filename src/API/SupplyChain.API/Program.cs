using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using SupplyChain.API.Extensions;
using SupplyChain.API.Middleware;
using SupplyChain.Common.Application;
using SupplyChain.Common.Infrastructure;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Orders.Infrastructure;
using SupplyChain.Modules.Users.Infrastructure;
using SupplyChain.Modules.Warehouses.Infrastructure;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//setup serilog step 1
//make sure na i lagay mo na yung serilog config sa configuration file
builder.Host.UseSerilog((context, loggerconfig) => { loggerconfig.ReadFrom.Configuration(context.Configuration); });


//need this for exception handle globally.. any uncatch handling will be handled by this middle ware
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


//config for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

//add mo yung applicatin common sa using statement and setup mo to. add mo yung array of assemble na babasahin nya
builder.Services.AddApplication([
    SupplyChain.Modules.Warehouses.Application.AssemblyReference.Assembly,
    SupplyChain.Modules.Users.Application.AssemblyReference.Assembly,
       SupplyChain.Modules.Orders.Application.AssemblyReference.Assembly
    ]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

//ad na naten yung COMMON infrastructure dependencies naten..will be used by our modules
builder.Services.AddInfrastructure(
    [WarehouseModule.ConfigureConsumers],
    databaseConnectionString,
    redisConnectionString);

//register na naten yung config file naten
builder.Configuration.AddModuleConfiguration(["supplychain","users","orders"]);


//health check build in na to
builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString);


//add na naten yung other needs ni module
builder.Services.AddWarehouseModule(builder.Configuration);
builder.Services.AddUserModule(builder.Configuration);
builder.Services.AddOrdersModule(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //for api na i apply nya new migrations naten
    app.ApplyMigrations();
}


//here is the time na iscan nya buon assembly mo para makita nya automatic yung mga endpoints mo IEndPoints
app.MapEndpoints();

//register an endpoint under sa path na to 'health'
app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});



//serilog step 2
app.UseSerilogRequestLogging();
app.UseExceptionHandler();



app.Run();


