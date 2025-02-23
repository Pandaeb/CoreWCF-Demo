using CoreWCF.Configuration;
using CoreWCF.Description;
using CoreWCF_Demo.Misc;
using CoreWCF_Demo_Infrastructure.AuthSchemes.UserPasswordScheme;
using CoreWCF_Demo_Infrastructure.Services;
using System.Text.Json;

namespace CoreWCF_Demo.Configuration
{
    public static class DependencyConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceModelServices().AddServiceModelMetadata();
            services.AddServiceModelWebServices();

            services.AddScoped<WcfService>();

            services.ConfigureBehaviors();
            services.ConfigureUserPasswordSettings(configuration);

            return services;
        }


        private static void ConfigureUserPasswordSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var userPasswordConfigurationSection = configuration.GetSection("UserPasswordAuthConfiguration");

            var userPasswordConfiguration = userPasswordConfigurationSection.Value is not null
                ? JsonSerializer.Deserialize<UserPasswordAuthConfiguration>(userPasswordConfigurationSection.Value!)!
                : userPasswordConfigurationSection.Get<UserPasswordAuthConfiguration>()!;

            services.AddSingleton(userPasswordConfiguration);
        }

        private static void ConfigureBehaviors(this IServiceCollection services)
        {
            services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
            services.AddSingleton<IServiceBehavior, CustomErrorServiceBehavior>();
        }
    }
}
