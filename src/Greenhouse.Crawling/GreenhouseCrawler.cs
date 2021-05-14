using System.Collections.Generic;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.Greenhouse.Core;
using CluedIn.Crawling.Greenhouse.Infrastructure.Factories;

namespace CluedIn.Crawling.Greenhouse
{
    public class GreenhouseCrawler : ICrawlerDataGenerator
    {
        private readonly IGreenhouseClientFactory _clientFactory;

        public GreenhouseCrawler(IGreenhouseClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        #region ICrawlerDataGenerator Members

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is GreenhouseCrawlJobData greenhousecrawlJobData))
            {
                yield break;
            }
            var client = _clientFactory.CreateNew(greenhousecrawlJobData);
            var users = client.GetUsers();
            foreach (var user in users)
            {
                yield return user;
            }
            foreach (var candidate in client.GetCandidates())
            {
                yield return candidate.educations;
                yield return candidate.attachments;
                yield return candidate.applications;
                yield return candidate.addresses;
                yield return candidate;
            }
            var jobs = client.GetJobs();
            foreach (var job in jobs)
            {
                yield return job;
            }
            var offices = client.GetOffices();
            foreach (var office in offices)
            {
                yield return office;
            }
            var offers = client.GetOffers();
            foreach (var offer in offers)
            {
                yield return offer;
            }
            var departments = client.GetDepartments();
            foreach (var department in departments)
            {
                yield return department;
            }
            var applications = client.GetApplications();
            foreach (var application in applications)
            {
                yield return application;
            }

            //retrieve data from provider and yield objects
        }

        #endregion
    }
}
