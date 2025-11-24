using MagicOnionChat.Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddAppDependencies(builder.Configuration);
builder.WebHost.ConfigureGrpcKestrel();

var app = builder.Build();

app.Configure();

app.Run();