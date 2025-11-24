using Cysharp.Runtime.Multicast.Distributed.Redis;
using MagicOnionChat.Backend.Infrastructure;
using MagicOnionChat.Backend.Infrastructure.Chat;
using StackExchange.Redis;

namespace MagicOnionChat.Backend.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddSingleton<ConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(
            configuration.GetConnectionString("Redis")
            ?? throw new InvalidOperationException("Redis not found")));

        services.AddMagicOnion()
            .UseRedisGroup(options =>
            {
                var multiplexer = services.BuildServiceProvider()
                    .GetRequiredService<ConnectionMultiplexer>();

                options.ConnectionMultiplexer = multiplexer;
            }, registerAsDefault: true);
        
        services.AddSingleton<ChatContextRepository>();
        
        services.AddHostedService<ChatGameLoopService>();
        
        return services;
    }
}