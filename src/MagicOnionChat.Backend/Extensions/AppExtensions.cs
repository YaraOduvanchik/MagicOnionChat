namespace MagicOnionChat.Backend.Extensions;

public static class AppExtensions
{
    public static IApplicationBuilder Configure(this WebApplication app)
    {
        app.MapMagicOnionService();
        
        return app;
    }
}