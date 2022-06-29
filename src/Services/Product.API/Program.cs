using Serilog;
using Common.Logging;
using Product.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Product.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting Product Api up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
   
    //Add config host
    builder.Host.AddAppConfiguration();

    //Add service
    builder.Services.AddInfrastructure(builder.Configuration);

    string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<ProductContext>();

    var app = builder.Build();
    app.UseInfrastructure();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhanlded exception");
}
finally
{
    Log.Information("Shut down product API complete");
    Log.CloseAndFlush();
}