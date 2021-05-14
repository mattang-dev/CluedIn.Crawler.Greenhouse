using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Greenhouse.Core;
using CluedIn.Crawling.Greenhouse.Core.Models;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;

namespace CluedIn.Crawling.Greenhouse.Infrastructure
{
    // TODO - This class should act as a client to retrieve the data to be crawled.
    // It should provide the appropriate methods to get the data
    // according to the type of data source (e.g. for AD, GetUsers, GetRoles, etc.)
    // It can receive a IRestClient as a dependency to talk to a RestAPI endpoint.
    // This class should not contain crawling logic (i.e. in which order things are retrieved)
    public class GreenhouseClient
    {
        private const string BaseUri = "https://harvest.greenhouse.io/v1/";
        private readonly GreenhouseCrawlJobData _crawlJobData;
        private readonly ILogger<GreenhouseClient> _logger;
        private readonly IRestClient _restClient;
        private string _lastcrawlDateStr;

        public GreenhouseClient(ILogger<GreenhouseClient> log, GreenhouseCrawlJobData crawlJobData, IRestClient client)
        {
            if (crawlJobData == null)
            {
                throw new ArgumentNullException(nameof(crawlJobData));
            }
            _lastcrawlDateStr = crawlJobData.LastCrawlFinishTime.ToString("o");
            _restClient = client ?? throw new ArgumentNullException(nameof(client));
            _logger = log ?? throw new ArgumentNullException(nameof(log));
            _restClient.BaseUrl = new Uri(BaseUri);
            _restClient.AddDefaultHeader("Authorization", $"Basic {crawlJobData.ApiKey}");
            _crawlJobData = crawlJobData;
            _restClient.Authenticator = new HttpBasicAuthenticator(_crawlJobData.ApiKey, "");
        }

        public IEnumerable<Candidate> GetCandidates()
        {
            var dataRetriever = new RestDataRetriever<Candidate>(_restClient, _crawlJobData);
            var results = dataRetriever.GetPagedData("candidates");
            return results;
        }

        public IEnumerable<Job> GetJobs()
        {
            var dataRetriever = new RestDataRetriever<Job>(_restClient, _crawlJobData);
            var results = dataRetriever.GetPagedData("jobs");
            return results;
        }

        public IEnumerable<Office> GetOffices()
        {
            var dataRetriever = new RestDataRetriever<Office>(_restClient, _crawlJobData);
            var results = dataRetriever.GetPagedData("offices");
            return results;
        }

        public IEnumerable<Offer> GetOffers()
        {
            var dataRetriever = new RestDataRetriever<Offer>(_restClient, _crawlJobData);
            var results = dataRetriever.GetPagedData("offers");
            return results;
        }

        public IEnumerable<Application> GetApplications()
        {
            var dataRetriever = new RestDataRetriever<Application>(_restClient, _crawlJobData);
            var results = dataRetriever.GetPagedData("applications");
            return results;
        }

        public IEnumerable<Department> GetDepartments()
        {
            var dataRetriever = new RestDataRetriever<Department>(_restClient, _crawlJobData);
            var results = dataRetriever.GetPagedData("departments");
            return results;
        }

        public AccountInformation GetAccountInformation()
        {
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation(_crawlJobData.ApiKey, "Greenhouse");
        }

        public IEnumerable<User> GetUsers()
        {
            var dataRetriever = new RestDataRetriever<User>(_restClient, _crawlJobData);
            var results = dataRetriever.GetPagedData("users");
            return results;
        }
    }

    public class RestDataRetriever<T> where T : new()
    {
        private readonly GreenhouseCrawlJobData _crawlJobData;
        private readonly string _lastCrawlDateStrIso8601;
        private readonly IRestClient _restClient;

        public RestDataRetriever(IRestClient restClient, GreenhouseCrawlJobData crawlJobData)
        {
            _restClient = restClient;
            _crawlJobData = crawlJobData;
            _lastCrawlDateStrIso8601 = _crawlJobData.LastCrawlFinishTime.ToString("o");
        }

        [GetHttpCall]
        public List<T> GetPagedData(string urlSegment, int perPage = 500)
        {
            var results = new List<T>();
            var page = 1;
            while (true)
            {
                var request = new RestRequest(urlSegment, Method.GET);
                _restClient.Authenticator = new HttpBasicAuthenticator(_crawlJobData.ApiKey, "");
                _restClient.AddDefaultHeader("Authorization", $"Basic {_crawlJobData.ApiKey}");
                request.AddQueryParameter("per_page", perPage.ToString());
                request.AddQueryParameter("page", page.ToString());
                request.AddQueryParameter("updated_after", _lastCrawlDateStrIso8601);
                var response = _restClient.Execute<IEnumerable<T>>(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
                results.AddRange(response.Data);
                page++;
                if (response.Data.Count() < perPage)
                {
                    break;
                }
            }
            return results;
        }
    }

    public class GetHttpCallAttribute : Attribute { }
}
