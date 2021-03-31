using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests
{
    [TestClass]
    class ServiceTests
    {
        private TrendController _TrendController;
        private Mock<ITrendBusinessLogic> trendBusinessLogicMock;

        public ServiceTests()
        {
            trendBusinessLogicMock = new Mock<ITrendBusinessLogic>();
            _TrendController = new TrendController(trendBusinessLogicMock.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsTheOnlyInstanceCreated()
        {
            //arrange
            TrendDto newTrendDto = new TrendDto();
            newTrendDto.Id = Guid.NewGuid();
            newTrendDto.Name = "sport";

            Trend newTrend = new Trend();
            newTrend.Id = (Guid)newTrendDto.Id;
            newTrend.Name = "sport";

            ICollection<TrendDto> allTrendDto = new List<TrendDto>();
            ICollection<TrendDto> allTrendDtoReturned = new List<TrendDto>();
            allTrendDto.Add(newTrendDto);


            trendBusinessLogicMock.Setup(trendBusinessLogicMock => trendBusinessLogicMock.GetAll()).Returns(allTrendDto);

            //act
            allTrendDtoReturned = _TrendController.GetAll();

            //assert
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Id, allTrendDto.ElementAt(0).Id);
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Name, allTrendDto.ElementAt(0).Name);
            Assert.IsTrue(allTrendDtoReturned.Count() > 0, "No trends returned");
            Assert.IsTrue(allTrendDtoReturned.Count() < 2, "More trends returned than existing");
        }

        [TestMethod]
        public void GetAll_ShouldBeIdempotent()
        {
            //arrange
            TrendDto newTrendDto = new TrendDto();
            newTrendDto.Id = Guid.NewGuid();
            newTrendDto.Name = "sport";

            Trend newTrend = new Trend();
            newTrend.Id = (Guid)newTrendDto.Id;
            newTrend.Name = "sport";

            ICollection<TrendDto> allTrendDto = new List<TrendDto>();
            ICollection<TrendDto> allTrendDtoReturned = new List<TrendDto>();
            allTrendDto.Add(newTrendDto);


            trendBusinessLogicMock.Setup(trendBusinessLogicMock => trendBusinessLogicMock.GetAll()).Returns(allTrendDto);

            //act
            allTrendDtoReturned = _TrendController.GetAll();
            allTrendDtoReturned = _TrendController.GetAll();
            allTrendDtoReturned = _TrendController.GetAll();
            allTrendDtoReturned = _TrendController.GetAll();

            //assert
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Id, allTrendDto.ElementAt(0).Id);
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Name, allTrendDto.ElementAt(0).Name);
            Assert.IsTrue(allTrendDtoReturned.Count() > 0, "No trends returned");
            Assert.IsTrue(allTrendDtoReturned.Count() < 2, "More trends returned than existing");
        }

        [TestMethod]
        public void Update_ChangesTrendData()
        {
            //arrange
            TrendDto newTrendDto = new TrendDto();
            newTrendDto.Id = Guid.NewGuid();
            newTrendDto.Name = "sport";

            Trend newTrend = new Trend();
            newTrend.Id = (Guid)newTrendDto.Id;
            newTrend.Name = "sport";

            TrendDto newTrendDto2 = new TrendDto();
            newTrendDto2.Id = (Guid)newTrendDto.Id;
            newTrendDto2.Name = "fashion";

            ICollection<TrendDto> allTrendDto = new List<TrendDto>();
            ICollection<TrendDto> allTrendDtoReturned = new List<TrendDto>();
            allTrendDto.Add(newTrendDto);


            trendBusinessLogicMock.Setup(trendBusinessLogicMock => trendBusinessLogicMock.GetById((Guid)newTrendDto.Id))
                                                                                         .Returns(newTrendDto);

            //act
            _TrendController.Create(newTrendDto);
            _TrendController.Update((Guid)newTrendDto.Id, newTrendDto2);

            //assert
            Assert.AreEqual(allTrendDtoReturned.ElementAt(0).Id, allTrendDto.ElementAt(0).Id);
            Assert.AreEqual("fashion", _TrendController.GetById((Guid)newTrendDto2.Id).Value.Name);
        }
    }
}
