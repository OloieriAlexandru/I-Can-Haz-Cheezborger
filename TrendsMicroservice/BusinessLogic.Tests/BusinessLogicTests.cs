using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.Abstractions;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests
{
    [TestClass]
    public class BusinessLogicTests
    {
        private ITrendBusinessLogic _TrendBusinessLogic;
        private Mock<IRepository<Trend>> trendRepositoryMock;


        public BusinessLogicTests()
        {
            trendRepositoryMock = new Mock<IRepository<Trend>>();
            _TrendBusinessLogic = new TrendBusinessLogic(trendRepositoryMock.Object);
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

            ICollection<Trend> allTrends = new List<Trend>();
            allTrends.Add(newTrend);
            ICollection<TrendDto> allTrendDto = new List<TrendDto>();
            

            trendRepositoryMock.Setup(trendRepositoryMock => trendRepositoryMock.GetAll()).Returns(allTrends);

            //act
            allTrendDto = _TrendBusinessLogic.GetAll();

            //assert
            Assert.AreEqual(newTrendDto.Id, allTrendDto.ElementAt(0).Id);
            Assert.AreEqual(newTrendDto.Name, allTrendDto.ElementAt(0).Name);
            Assert.IsTrue(allTrendDto.Count() > 0, "No trends returned");
            Assert.IsTrue(allTrendDto.Count() < 2, "More trends returned than existing");
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

            ICollection<Trend> allTrends = new List<Trend>();
            allTrends.Add(newTrend);
            ICollection<TrendDto> allTrendDto = new List<TrendDto>();


            trendRepositoryMock.Setup(trendRepositoryMock => trendRepositoryMock.GetAll()).Returns(allTrends);

            //act
            allTrendDto = _TrendBusinessLogic.GetAll();
            allTrendDto = _TrendBusinessLogic.GetAll();
            allTrendDto = _TrendBusinessLogic.GetAll();
            allTrendDto = _TrendBusinessLogic.GetAll();

            //assert
            Assert.AreEqual(newTrendDto.Id, allTrendDto.ElementAt(0).Id);
            Assert.AreEqual(newTrendDto.Name, allTrendDto.ElementAt(0).Name);
            Assert.IsTrue(allTrendDto.Count() > 0, "No trends returned");
            Assert.IsTrue(allTrendDto.Count() < 2, "More trends returned than existing");
        }

        [TestMethod]
        public void GetById_ReturnsCreatedInstance()
        {
            //arrange
            TrendDto newTrendDto = new TrendDto();
            newTrendDto.Id = Guid.NewGuid();
            newTrendDto.Name = "sport";

            Trend newTrend = new Trend();
            newTrend.Id = (Guid)newTrendDto.Id;
            newTrend.Name = "sport";


            trendRepositoryMock.Setup(trendRepositoryMock => trendRepositoryMock.GetById((Guid)newTrendDto.Id))
                                                                                .Returns(newTrend);

            //act
            TrendDto trendReturned = _TrendBusinessLogic.GetById((Guid)newTrendDto.Id);

            //assert
            Assert.AreEqual(newTrendDto.Id, trendReturned.Id);
            Assert.AreEqual(newTrendDto.Name, trendReturned.Name);
        }

        [TestMethod]
        public void Update_ChangesTrendData()
        {
            //arrange
            TrendDto newTrendDto = new TrendDto();
            newTrendDto.Id = Guid.NewGuid();
            newTrendDto.Name = "sport";

            Trend newTrend2 = new Trend();
            newTrend2.Id = (Guid)newTrendDto.Id;
            newTrend2.Name = "fashion";

            TrendDto newTrendDto2 = new TrendDto();
            newTrendDto2.Id = (Guid)newTrendDto.Id;
            newTrendDto2.Name = "fashion";

            trendRepositoryMock.Setup(trendRepositoryMock => trendRepositoryMock.GetById((Guid)newTrendDto2.Id))
                                                                                .Returns(newTrend2);

            //act
            _TrendBusinessLogic.Create(newTrendDto);
            _TrendBusinessLogic.Update(newTrendDto2);

            //assert
            Assert.AreEqual("fashion", _TrendBusinessLogic.GetById((Guid)newTrendDto2.Id).Name);
        }
    }
}
