using Microsoft.Extensions.DependencyInjection;
using OpenReferrals.Connectors.Common;
using OpenReferrals.RegisterManagementConnector.Configuration;
using OpenReferrals.RegisterManagementConnector.ServiceClients;


namespace OpenReferrals.RegisterManagementConnector.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InjectRegisterManagementServiceClient(this IServiceCollection services, RegisterManagmentOptions options)
        {
            services.AddHttpClient<IHttpClientAdapter, HttpClientAdapter>();
            services.AddSingleton(options);
            services.AddTransient<IHttpClientAdapter, HttpClientAdapter>();
            services.AddTransient<IRegisterManagmentServiceClient, RegisterManagementServiceClient>();
            return services;
        }
    }
}
