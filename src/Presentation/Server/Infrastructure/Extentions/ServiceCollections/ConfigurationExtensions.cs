using Microsoft.Extensions.Options;
using Server.Infrastructure.Settings;

namespace Server.Infrastructure.Extensions.ServiceCollections
{
    public static class ConfigurationExtensions
	{
		public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
		{
			builder.Services.Configure<ApplicationSettings>
			 (builder.Configuration.GetSection(key: ApplicationSettings.KeyName))
			 .AddSingleton
			 (implementationFactory: serviceType =>
			 {
				 var result =
					 serviceType.GetRequiredService
					 <IOptions<ApplicationSettings>>().Value;

				 return result;
			 });

			 return builder;
		}
	}
}
