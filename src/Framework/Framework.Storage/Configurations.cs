using Microsoft.Extensions.DependencyInjection;

namespace Framework.Storage;

public static class Configurations
{
    public static void AddS3Storage(this IServiceCollection services, StorageConfig storageConfig)
    {
        services.AddSingleton(storageConfig);    
        services.AddScoped<IStorage, AmazonS3>();
    }
    
}
