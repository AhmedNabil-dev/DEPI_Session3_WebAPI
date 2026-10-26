using DEPI_Session3_Web.Extensions;
using DEPI_WebApi_Session2.Helper;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Seeding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddServies(builder.Configuration);


var app = builder.Build();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    var context = services.GetRequiredService<ApplicationDBContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeeding.SeedDataAsync(context);

}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred during migration");
}


    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
