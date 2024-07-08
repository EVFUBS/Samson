using Microsoft.EntityFrameworkCore;
using SamsonLocal;
using SamsonLocal.Context;
using SamsonLocal.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Environment.EnvironmentName = "development";
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddDbContext<SamsonContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("Samsondb")));

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddHostedService<SpeechRecognitionHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
