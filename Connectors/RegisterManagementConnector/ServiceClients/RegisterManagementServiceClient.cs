using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Configuration;
using OpenReferrals.RegisterManagementConnector.Models;
using OpenReferrals.Connectors.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenReferrals.RegisterManagementConnector.ServiceClients
{
    public class RegisterManagementServiceClient : IRegisterManagmentServiceClient
    {
        private readonly RegisterManagmentOptions _options;
        private readonly IHttpClientAdapter _httpClient;
        private readonly IConfiguration _config;
        public RegisterManagementServiceClient(RegisterManagmentOptions options, IHttpClientAdapter httpClient, IConfiguration config)
        {
            _options = options;
            _httpClient = httpClient;
            _config = config;
        }

        public Organisation CreateOrganisation(Organisation organisation)
        {
            // add id to be used by siccar
            organisation.Id = Guid.NewGuid().ToString();

            //Only connect with siccar when the app is deployed.
            if (bool.Parse(_config["ConnectToSiccar"]))
            {
                // We don't await this we pray to the Siccar gods that everyting works
                var endpoint = new Uri($"{_config["RegisterAPI:BaseUrl"]}/OpenReferrals");
                var siccarOrg = new SiccarOrganisation(organisation);

                _httpClient.PostRequest(endpoint, siccarOrg);
            }
            return organisation;
        }

        public Service CreateService(Service service)
        {

            //Only connect with siccar when the app is deployed.
            if (bool.Parse(_config["ConnectToSiccar"]))
            {
                // We don't await this we pray to the Siccar gods that everyting works
                var endpoint = new Uri($"{_config["RegisterAPI:BaseUrl"]}/OpenReferrals");
                //var siccarSer = new SiccarOrganisation(service);

                //_httpClient.RegisterPostRequest(endpoint, siccarSer);
            }
            return service;
        }

        public Organisation UpdateOrganisation(Organisation organisation)
        {
            //Only connect with siccar when the app is deployed.
            if (bool.Parse(_config["ConnectToSiccar"]))
            {
                // We don't await this we pray to the Siccar gods that everyting works
                var endpoint = new Uri($"{_config["RegisterAPI:BaseUrl"]}/OpenReferrals/{organisation.Id}");
                var siccarOrg = new SiccarOrganisation(organisation);

                _httpClient.PostRequest(endpoint, siccarOrg);
            }
            return organisation;
        }
    }
}
