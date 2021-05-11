using System;
using System.Linq;
using CluedIn.Crawling.Greenhouse.Core;
using CluedIn.Crawling.Greenhouse.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using RestSharp;
using Xunit;

namespace CluedIn.Crawling.Greenhouse.Integration.Test
{
    public class GreenhouseClientTests
    {
        [Fact]
        public void GetCandidatesShouldReturnDataByPages()
        {
            var mockLogger = new Mock<ILogger<GreenhouseClient>>();
            var greenhouseCrawlJobData = new GreenhouseCrawlJobData
            {
                LastCrawlFinishTime = DateTimeOffset.UtcNow.AddYears(-1), // one year ago
                ApiKey = "cea405ac4796defb9ab7dea89f8f09fb-4"
            };
            var greenhouseClient = new GreenhouseClient(mockLogger.Object, greenhouseCrawlJobData,
                new RestClient("https://harvest.greenhouse.io/v1/"));
            var candidates = greenhouseClient.GetCandidates(greenhouseCrawlJobData.LastCrawlFinishTime);
            Assert.True(candidates.Any());
        }
    }
}
