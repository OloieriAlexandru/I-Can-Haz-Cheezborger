using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.Abstractions;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Tests
{
    [TestClass]
    public class TrendsBusinessLogicTests : BaseBusinessLogicTests
    {
        private readonly Mock<IRepository<Trend>> trendRepositoryMock;

        private readonly ITrendBusinessLogic systemUnderTest;

        private readonly Trend testTrend = new Trend()
        {
            Id = Guid.Parse("fdc94950-cf1a-4eee-ad9a-53748046087f"),
            Name = "TestTrend",
            Description = "TestTrendDescription",
            ImageUrl = "TestTrendImageUrl"
        };

        private readonly TrendGetAllDto testTrendGetAllDto = new TrendGetAllDto()
        {
            Id = Guid.Parse("fdc94950-cf1a-4eee-ad9a-53748046087f"),
            Name = "TestTrend",
            Description = "TestTrendDescription",
            ImageUrl = "TestTrendImageUrl"
        };

        private readonly TrendGetByIdDto testTrendGetByIdDto = new TrendGetByIdDto()
        {
            Id = Guid.Parse("fdc94950-cf1a-4eee-ad9a-53748046087f"),
            Name = "TestTrend",
            Description = "TestTrendDescription",
            ImageUrl = "TestTrendImageUrl"
        };

        public TrendsBusinessLogicTests(): base()
        {
            trendRepositoryMock = new Mock<IRepository<Trend>>();
            systemUnderTest = new TrendBusinessLogic(trendRepositoryMock.Object, mapper);
        }

        [TestMethod]
        public void GetAll_ReturnsTheOnlyInstanceCreated()
        {
            // Arrange
            ICollection<Trend> trends = new List<Trend> { testTrend };
            trendRepositoryMock.Setup(x => x.GetAll()).Returns(trends);
            ICollection<TrendGetAllDto> expectedTrends = new List<TrendGetAllDto> { testTrendGetAllDto };

            // Act
            ICollection<TrendGetAllDto> returnedTrends = systemUnderTest.GetAll();

            // Assert
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expectedTrends, (System.Collections.ICollection)returnedTrends);
        }

        [TestMethod]
        public void GetById_ReturnsCreatedInstance()
        {
            // Arrange
            trendRepositoryMock.Setup(x => x.GetById(testTrend.Id)).Returns(testTrend);

            // Act
            TrendGetByIdDto returnedTrend = systemUnderTest.GetById(testTrend.Id);

            // Assert
            Assert.AreEqual(testTrendGetByIdDto, returnedTrend);
        }
    }
}
