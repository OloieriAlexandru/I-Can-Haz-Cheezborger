using BusinessLogic.Abstractions;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Tests
{
    [TestClass]
    public class TrendsControllerTests
    {
        private Mock<ITrendBusinessLogic> trendBusinessLogicMock;

        private TrendsController systemUnderTest;

        public TrendsControllerTests()
        {
            trendBusinessLogicMock = new Mock<ITrendBusinessLogic>();
            systemUnderTest = new TrendsController(trendBusinessLogicMock.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsTheOnlyInstanceCreated()
        {
            // Arrange
            TrendDto newTrendDto = new TrendDto { Id = Guid.NewGuid(), Name = "sport" };
            Trend newTrend = new Trend { Id = (Guid)newTrendDto.Id, Name = "sport" };
            ICollection<TrendDto> allTrendDto = new List<TrendDto>();
            ICollection<TrendDto> allTrendDtoReturned = new List<TrendDto>();
            allTrendDto.Add(newTrendDto);
            trendBusinessLogicMock.Setup(trendBusinessLogicMock => trendBusinessLogicMock.GetAll()).Returns(allTrendDto);

            // Act
            allTrendDtoReturned = systemUnderTest.GetAll();

            // Assert
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Id, allTrendDto.ElementAt(0).Id);
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Name, allTrendDto.ElementAt(0).Name);
            Assert.IsTrue(allTrendDtoReturned.Count() > 0, "No trends returned");
            Assert.IsTrue(allTrendDtoReturned.Count() < 2, "More trends returned than existing");
        }

        [TestMethod]
        public void GetAll_ShouldBeIdempotent()
        {
            // Arrange
            TrendDto newTrendDto = new TrendDto { Id = Guid.NewGuid(), Name = "sport" };
            ICollection<TrendDto> allTrendDto = new List<TrendDto>();
            ICollection<TrendDto> allTrendDtoReturned = new List<TrendDto>();
            allTrendDto.Add(newTrendDto);
            trendBusinessLogicMock.Setup(trendBusinessLogicMock => trendBusinessLogicMock.GetAll()).Returns(allTrendDto);

            // Act
            allTrendDtoReturned = systemUnderTest.GetAll();
            allTrendDtoReturned = systemUnderTest.GetAll();
            allTrendDtoReturned = systemUnderTest.GetAll();
            allTrendDtoReturned = systemUnderTest.GetAll();

            // Assert
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Id, allTrendDto.ElementAt(0).Id);
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Name, allTrendDto.ElementAt(0).Name);
            Assert.IsTrue(allTrendDtoReturned.Count() > 0, "No trends returned");
            Assert.IsTrue(allTrendDtoReturned.Count() < 2, "More trends returned than existing");
        }

        [TestMethod]
        public void Update_ChangesTrendData()
        {
            // Arrange
            TrendDto newTrendDto = new TrendDto { Id = Guid.NewGuid(), Name = "sport" };
            TrendDto newTrendDtoUpdated = new TrendDto { Id = (Guid)newTrendDto.Id, Name = "fashion" };

            // Act
            IActionResult resultBadRequest = systemUnderTest.Update(Guid.NewGuid(), newTrendDtoUpdated);
            IActionResult noContentRequest = systemUnderTest.Update((Guid)newTrendDto.Id, newTrendDtoUpdated);

            // Assert
            Assert.IsInstanceOfType(resultBadRequest, typeof(BadRequestResult));
            Assert.IsInstanceOfType(noContentRequest, typeof(NoContentResult));
        }
    }
}
