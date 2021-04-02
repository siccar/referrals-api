using Microsoft.Extensions.DependencyInjection;
using OpenReferrals.Policies.HttpPolicies;
using OpenReferrals.RegisterManagementConnector.Configuration;
using OpenReferrals.RegisterManagementConnector.ServiceClients;


namespace OpenReferrals.RegisterManagementConnector.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InjectRegisterManagementServiceClient(this IServiceCollection services, RegisterManagmentOptions options)
        {
            services.AddHttpClient<IHttpClientAdapter, HttpClientAdapter>(PolicyNames.RegisterHttpClient)
                .AddPolicyHandler(HttpPolicies.GetRetryPolicy());
            services.AddSingleton(options);
            services.AddTransient<IHttpClientAdapter, HttpClientAdapter>();
            services.AddTransient<IRegisterManagmentServiceClient, RegisterManagementServiceClient>();
            return services;
        }
    }
}
