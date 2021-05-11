using System;
using System.Collections;
using System.Net;
using System.Threading.Tasks;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Greenhouse.Core;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.Extensions.Logging;
using RestSharp.Authenticators;
using CluedIn.Crawling.Greenhouse.Core.Models;
using System.Collections.Generic;

namespace CluedIn.Crawling.Greenhouse.Infrastructure
{
    // TODO - This class should act as a client to retrieve the data to be crawled.
    // It should provide the appropriate methods to get the data
    // according to the type of data source (e.g. for AD, GetUsers, GetRoles, etc.)
    // It can receive a IRestClient as a dependency to talk to a RestAPI endpoint.
    // This class should not contain crawling logic (i.e. in which order things are retrieved)
    public class GreenhouseClient
    {
        private const string BaseUri = "https://harvest.greenhouse.io/";

        private readonly ILogger<GreenhouseClient> log;

        private readonly IRestClient client;

        private readonly GreenhouseCrawlJobData _greenhouseCrawlJobData;

        public GreenhouseClient(ILogger<GreenhouseClient> log, GreenhouseCrawlJobData greenhouseCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
        {
            if (greenhouseCrawlJobData == null)
            {
                throw new ArgumentNullException(nameof(greenhouseCrawlJobData));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.client = client ?? throw new ArgumentNullException(nameof(client));

            // TODO use info from greenhouseCrawlJobData to instantiate the connection
            client.BaseUrl = new Uri(BaseUri);
            // client.AddDefaultParameter("api_key", greenhouseCrawlJobData.ApiKey, ParameterType.QueryString);
            client.AddDefaultHeader("Authorization", $"Basic {greenhouseCrawlJobData.ApiKey}");
            _greenhouseCrawlJobData = greenhouseCrawlJobData;
        }

        public IEnumerable<Candidate> GetCandidates(DateTimeOffset lastCrawlDatetime)
        {
            var results = new List<Candidate>();
            var perPage = 5;
            var page = 1;
            var lastcrawlDateStr = lastCrawlDatetime.ToString("o");
            var client = new RestClient("https://harvest.greenhouse.io/v1");
            while (true)
            {
                var request = new RestRequest("candidates", Method.GET);
                client.Authenticator = new HttpBasicAuthenticator(_greenhouseCrawlJobData.ApiKey, "");
                request.AddQueryParameter("per_page", perPage.ToString());
                request.AddQueryParameter("page", page.ToString());
                request.AddQueryParameter("updated_after", lastcrawlDateStr);
                var response = client.Execute<List<Candidate>>(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
                
                if (response.Data.Count < perPage)
                {
                    break;
                }
            }
            return results;
        }



        public IEnumerable<Job> GetJobs(DateTimeOffset jobDataLastCrawlFinishTime)
        {
            var client = new RestClient("https://harvest.greenhouse.io/v1");
            var request = new RestRequest("jobs", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(_greenhouseCrawlJobData.ApiKey, "");
            var response = client.Execute<List<Job>>(request);
            var content = response.Data;
            return content;
        }


        public IEnumerable<Office> GetOffices(DateTimeOffset jobDataLastCrawlFinishTime)
        {
            var client = new RestClient("https://harvest.greenhouse.io/v1");
            var request = new RestRequest("offices", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(_greenhouseCrawlJobData.ApiKey, "");
            var response = client.Execute<List<Office>>(request);
            var content = response.Data;
            return content;
        }

        public IEnumerable<Offer> GetOffers(DateTimeOffset jobDataLastCrawlFinishTime)
        {
            var client = new RestClient("https://harvest.greenhouse.io/v1");
            var request = new RestRequest("offers", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(_greenhouseCrawlJobData.ApiKey, "");
            var response = client.Execute<List<Offer>>(request);
            var content = response.Data;
            return content;
        }

        public IEnumerable<Application> GetApplications(DateTimeOffset lastCrawlDatetime)
        {
            var client = new RestClient("https://harvest.greenhouse.io/v1");

            List<Application> results = new List<Application>();

            var request = new RestRequest("applications", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(_greenhouseCrawlJobData.ApiKey, "");
            var response = client.Execute<List<Application>>(request);
            var content = response.Data;

            // loop for paginated Data




            return content;
        }

        public IEnumerable<Department> GetDepartments(DateTimeOffset lastCrawlDatetime)
        {
            var client = new RestClient("https://harvest.greenhouse.io/v1");
            var request = new RestRequest("departments", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(_greenhouseCrawlJobData.ApiKey, "");
            request.AddQueryParameter("updated_after", lastCrawlDatetime.ToString("o"));
            var response = client.Execute<List<Department>>(request);
            var content = response.Data;
            return content;
        }
        public AccountInformation GetAccountInformation()
        {
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation(_greenhouseCrawlJobData.ApiKey, "Greenhouse");
        }

        public IEnumerable<User> GetUsers(DateTimeOffset lastCrawlDatetime)
        {
            var client = new RestClient("https://harvest.greenhouse.io/v1");
            var request = new RestRequest("users", Method.GET);
            request.AddQueryParameter("updated_after", lastCrawlDatetime.ToString("o"));
            client.Authenticator = new HttpBasicAuthenticator(_greenhouseCrawlJobData.ApiKey, "");
            var response = client.Execute<List<User>>(request);
            var content = response.Data;
            return content;
        }
    }
}
