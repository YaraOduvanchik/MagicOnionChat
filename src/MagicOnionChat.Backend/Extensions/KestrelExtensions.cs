using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace MagicOnionChat.Backend.Extensions;

public static class KestrelExtensions
{
    public static IWebHostBuilder ConfigureGrpcKestrel(this IWebHostBuilder builder)
    {
        var grpcPort = Environment.GetEnvironmentVariable("CHAT_PORT") ?? "8081"; 
        
        builder.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(int.Parse(grpcPort), listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });
        
        return builder;
    }
}