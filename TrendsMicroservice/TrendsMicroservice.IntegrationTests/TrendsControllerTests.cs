using Models.Trends;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TrendsMicroservice.IntegrationTests.IntegrationTestsUtil;
using Xunit;

namespace TrendsMicroservice.IntegrationTests
{
    public class TrendsControllerTests : BaseIntegrationTests
    {
        private List<TrendCreateDto> trendsToBeCreatedList;

        public TrendsControllerTests() : base()
        {
            trendsToBeCreatedList = new List<TrendCreateDto>()
            {
                new TrendCreateDto()
                {
                    Name = "FirstTestTrendName",
                    Description = "FirstTestTrendDescription",
                    ImageUrl = "FirstTestTrendImageUrl"
                },
                new TrendCreateDto()
                {
                    Name = "SecondTestTrendName",
                    Description = "SecondTestTrendDescription",
                    ImageUrl = "SecondTestTrendImageUrl"
                }
            };
        }
        
        [Fact]
        public async Task GetAll_WithMultipleTrends_ReturnsAllTheTrends()
        {
            await CreateTrends(trendsToBeCreatedList);

            var request = HttpRequestsMessagesFactory.Get(HttpMethod.Get, ApiRoutes.Trends.GetAll);
            var response = await TestHttpClient.SendAsync(request);

            List<TrendGetAllDto> trends = JsonConvert.DeserializeObject<List<TrendGetAllDto>>(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(trends.Count >= trendsToBeCreatedList.Count);
        }

        [Fact]
        public async Task Create_WithValidTrend_SuccessfullyCreatesTrend()
        {
            TrendCreateDto trend = trendsToBeCreatedList[0];

            var request = HttpRequestsMessagesFactory.Get(HttpMethod.Post, ApiRoutes.Trends.Create, trend);
            var response = await TestHttpClient.SendAsync(request);

            TrendGetAllDto returnedTrend = JsonConvert.DeserializeObject<TrendGetAllDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(trend.Name, returnedTrend.Name);
            Assert.Equal(trend.Description, returnedTrend.Description);
        }

        private async Task CreateTrends(List<TrendCreateDto> trendsToBeCreatedList)
        {
            foreach(TrendCreateDto trend in trendsToBeCreatedList)
            {
                var request = HttpRequestsMessagesFactory.Get(HttpMethod.Post, ApiRoutes.Trends.Create, trend);
                var response = await TestHttpClient.SendAsync(request);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
    }
}
