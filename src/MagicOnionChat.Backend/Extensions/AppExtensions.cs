
using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Extensions;

public static class AppExtensions
{
    public static IApplicationBuilder Configure(this WebApplication app)
    {
        app.MapMagicOnionService(typeof(ChatHub).Assembly);
        
        return app;
    }
}