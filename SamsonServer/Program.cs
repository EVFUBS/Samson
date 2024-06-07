using Hangfire;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using SamsonConsoleApp.Context;
using SamsonServer;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddDbContext<SamsonContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Samsondb")));

builder.Services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                                           .UseSimpleAssemblyNameTypeSerializer()
                                                           .UseRecommendedSerializerSettings()
                                                           .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

builder.Services.AddControllers();

builder.Services.AddPredictionEnginePool<SamsonActionModel.SamsonActionClassification.ModelInput, SamsonActionModel.SamsonActionClassification.ModelOutput>()
    .FromFile("C:\\Users\\lssmith\\Documents\\pdrepos\\Samson\\SamsonConsoleApp\\SamsonActionModel\\SamsonActionClassification.mlnet");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "SamsonUI",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000");
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("SamsonUI");

if (app.Environment.IsDevelopment())
{
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        DashboardTitle = "Samson Server Dashboard",
    });
}
app.UseDeveloperExceptionPage();

app.MapControllers();

app.Run();
