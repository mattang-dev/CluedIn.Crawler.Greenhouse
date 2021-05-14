using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CluedIn.Crawling.Greenhouse.Core;
using CluedIn.Crawling.Greenhouse.Core.Models;
using CluedIn.Crawling.Greenhouse.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace CluedIn.Crawling.Greenhouse.Integration.Test
{
    public class GreenhouseClientTests : IntegrationTest
    {
        private readonly ITestOutputHelper _console;

        public GreenhouseClientTests(ITestOutputHelper console)
        {
            _console = console;
        }
        [Fact]
  
        public void GetCandidatesShouldReturnDataByPages()
        {
            var mockLogger = NullLogger<GreenhouseClient>.Instance;
            var greenhouseCrawlJobData = new GreenhouseCrawlJobData
            {
                LastCrawlFinishTime = DateTimeOffset.UtcNow.AddDays(-7), // one year ago
                ApiKey = "cea405ac4796defb9ab7dea89f8f09fb-4"
            };
            var greenhouseClient = new GreenhouseClient(mockLogger, greenhouseCrawlJobData,
                new RestClient("https://harvest.greenhouse.io/v1/"));
            var candidates = greenhouseClient.GetCandidates();
            Assert.True(candidates.Any());
            _console.WriteLine($"Candidates: {candidates.Count()}");
        }

        
    }

    [Trait("Category","Integration")]
    public class IntegrationTest { }
}
