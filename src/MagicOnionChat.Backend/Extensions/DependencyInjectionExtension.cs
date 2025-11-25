using MagicOnionChat.Backend.Abstractions;
using MagicOnionChat.Backend.BackgroundServices;
using MagicOnionChat.Backend.Processing;

namespace MagicOnionChat.Backend.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddInfrastructure(configuration)
            .AddChatFeatures();
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddMagicOnion()
            .UseRedisGroup(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("Redis")
                    ?? throw new InvalidOperationException("Redis not found");
            }, registerAsDefault: true);

        return services;
    }

    private static IServiceCollection AddChatFeatures(this IServiceCollection services)
    {
        services.AddSingleton<ChatContextFactory>();
        services.AddSingleton<IChatCommandProcessor, ChatCommandProcessor>();

        services.AddHostedService<ChatGameLoopService>();
        services.AddHostedService<ChatNotificationService>();

        return services;
    }
}
