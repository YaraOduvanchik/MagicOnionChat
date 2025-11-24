using MagicOnionChat.Backend.Abstractions;
using MagicOnionChat.Backend.BackgroundServices;
using MagicOnionChat.Backend.ExceptionHandlers;
using MagicOnionChat.Backend.Repositories;

namespace MagicOnionChat.Backend.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

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
        services.AddSingleton<IChatContextRepository, ChatContextRepository>();

        services.AddHostedService<ChatGameLoopService>();
        services.AddHostedService<ChatNotificationService>();

        return services;
    }
}
