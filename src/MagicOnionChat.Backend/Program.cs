using MagicOnionChat.Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppDependencies(builder.Configuration);

var app = builder.Build();

app.Configure();

app.Run();