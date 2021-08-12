using Infrastructure.Integrations.Typicode;
using Infrastructure.Integrations.Typicode.Interfaces;
using Infrastructure.Services.Gallery;
using Infrastructure.Services.Gallery.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            AddIoC(services);

            services.AddSingleton<IRestClient, RestClient>();


            return services;
        }

        private static void AddIoC(IServiceCollection services)
        {
            services.AddSingleton<ITypicodeConfigurations, TypicodeConfigurations>();
            services.AddSingleton<ITypicodeClient, TypicodeClient>();

            services.AddSingleton<IGalleryService, GalleryService>();
        }

    }
}
