using System.Linq;
using CrawlerIntegrationTesting.Clues;
using Xunit;
using Xunit.Abstractions;

namespace CluedIn.Crawling.Greenhouse.Integration.Test
{
    public class DataIngestion : IntegrationTest, IClassFixture<GreenhouseTestFixture>
    {
        private readonly GreenhouseTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public DataIngestion(GreenhouseTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Theory]
        [InlineData("/Provider/Root", 1)]
        //TODO: Add details for the count of entityTypes your test produces
        //[InlineData("SOME_ENTITY_TYPE", 1)]
        public void CorrectNumberOfEntityTypes(string entityType, int expectedCount)
        {
            var foundCount = _fixture.ClueStorage.CountOfType(entityType);

            //You could use this method to output the logs inside the test case
            _output.WriteLine($"entity types: {foundCount}");
            _fixture.PrintLogs(_output);

            Assert.Equal(expectedCount, foundCount);
        }

        [Fact]
        public void EntityCodesAreUnique()
        {
            var count = _fixture.ClueStorage.Clues.Count();
            var unique = _fixture.ClueStorage.Clues.Distinct(new ClueComparer()).Count();

            //You could use this method to output info of all clues
            _fixture.PrintClues(_output);

            Assert.Equal(unique, count);
        }

       
    }
}
