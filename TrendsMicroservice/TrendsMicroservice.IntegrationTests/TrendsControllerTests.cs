using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Newtonsoft.Json;
using System;
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
        private List<TrendDto> trendsList;

        [TestInitialize]
        public void TestInitialize()
        {
            trendsList = new List<TrendDto>()
            {
                new TrendDto()
                {
                    Id = Guid.Parse("0659919a-5f6f-4dad-90b0-e5b0339278c4"),
                    Name = "FirstTestTrend"
                },
                new TrendDto()
                {
                    Id = Guid.Parse("1692207b-adce-4605-9b5e-52868030e710"),
                    Name = "SecondTestTrend"
                }
            };
        }
        
        [TestMethod]
        public async Task GetAll_WithMultipleTrends_ReturnsAllTheTrends()
        {
            // Arrange
            await CreateTrends(trendsList);

            // Act
            var response = await TestHttpClient.GetAsync(ApiRoutes.Trends.GetAll);

            // Assert
            List<TrendDto> trends = JsonConvert.DeserializeObject<List<TrendDto>>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(trends.Count > trendsList.Count);
        }

        [TestMethod]
        public async Task Create_WithValidTrend_SuccessfullyCreatesTrend()
        {
            // Arrange
            TrendDto trend = new TrendDto()
            {
                Name = "TestTrend"
            };

            // Act
            var response = await TestHttpClient.PostAsJsonAsync(ApiRoutes.Trends.Create, trend);

            // Assert
            TrendDto returnedTrend = JsonConvert.DeserializeObject<TrendDto>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.IsNotNull(returnedTrend.Id);
        }

        private async Task CreateTrends(List<TrendDto> trends)
        {
            foreach(TrendDto trend in trends)
            {
                var response = await TestHttpClient.PostAsJsonAsync(ApiRoutes.Trends.Create, trend);
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            }
        }
    }
}
