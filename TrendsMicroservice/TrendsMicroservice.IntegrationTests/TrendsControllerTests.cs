using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrendsMicroservice.IntegrationTests.IntegrationTestsUtil;

namespace TrendsMicroservice.IntegrationTests
{
    [TestClass]
    public class TrendsControllerTests : BaseIntegrationTests
    {
        private List<TrendCreateDto> trendsToBeCreatedList;

        [TestInitialize]
        public void TestInitialize()
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
        
        [TestMethod]
        public async Task GetAll_WithMultipleTrends_ReturnsAllTheTrends()
        {
            // Arrange
            await CreateTrends(trendsToBeCreatedList);

            // Act
            var response = await TestHttpClient.GetAsync(ApiRoutes.Trends.GetAll);

            // Assert
            List<TrendGetAllDto> trends = JsonConvert.DeserializeObject<List<TrendGetAllDto>>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(trends.Count >= trendsToBeCreatedList.Count);
        }

        [TestMethod]
        public async Task Create_WithValidTrend_SuccessfullyCreatesTrend()
        {
            // Arrange
            TrendCreateDto trend = trendsToBeCreatedList[0];

            // Act
            var response = await TestHttpClient.PostAsJsonAsync(ApiRoutes.Trends.Create, trend);

            // Assert
            TrendGetAllDto returnedTrend = JsonConvert.DeserializeObject<TrendGetAllDto>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.IsNotNull(returnedTrend.Id);
        }

        private async Task CreateTrends(List<TrendCreateDto> trendsToBeCreatedList)
        {
            foreach(TrendCreateDto trend in trendsToBeCreatedList)
            {
                var response = await TestHttpClient.PostAsJsonAsync(ApiRoutes.Trends.Create, trend);
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            }
        }
    }
}
