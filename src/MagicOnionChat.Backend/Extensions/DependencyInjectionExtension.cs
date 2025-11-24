using MagicOnionChat.Backend.BackgroundServices;
using MagicOnionChat.Backend.ExceptionHandlers;
using MagicOnionChat.Backend.Repositories;
using StackExchange.Redis;

namespace MagicOnionChat.Backend.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        return services
            .AddInfrastructure(configuration)
            .AddChatFeature();
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddSingleton<ConnectionMultiplexer>(_ => 
            ConnectionMultiplexer.Connect(
                configuration.GetConnectionString("Redis")
                ?? throw new InvalidOperationException("Redis not found")));

        services.AddMagicOnion()
            .UseRedisGroup(options =>
            {
                var multiplexer = services.BuildServiceProvider()
                    .GetRequiredService<ConnectionMultiplexer>();

                options.ConnectionMultiplexer = multiplexer;
            }, registerAsDefault: true);

        return services;
    }

    private static IServiceCollection AddChatFeature(this IServiceCollection services)
    {
        services.AddSingleton<ChatContextRepository>();
        
        services.AddHostedService<ChatGameLoopService>();
        services.AddHostedService<ChatNotificationService>();
        
        return services;
    }
}
