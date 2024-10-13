using battleshipAPI.Data;
using battleshipAPI.Domain.Service;
using battleshipAPI.Service;
using battleshipAPI.Service.Interfaces;

using BattleshipAPI.Common;
using BattleshipAPI.Service;
using BattleshipAPI.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var policy = "SpecificOriginPolicy";
var webURL = builder.Configuration["WebURL"];
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy,
        builder =>
        {
            builder.WithOrigins(webURL).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PlayerCoordinates>();
builder.Services.AddSingleton<BotCoordinates>();

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddScoped<IFireService, FireService>();
builder.Services.AddScoped<ICoordinateValidator, CoordinateValidator>();
builder.Services.AddScoped<IBaseShipValidator, BaseShipValidator>();

builder.Services.Configure<GridSettings>(builder.Configuration.GetSection(GridSettings.GridSettingsPath));
builder.Services.Configure<ClassOfShips>(builder.Configuration.GetSection(ClassOfShips.ClassOfShipsPath));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy);

app.UseAuthorization();

app.MapControllers();

app.Run();