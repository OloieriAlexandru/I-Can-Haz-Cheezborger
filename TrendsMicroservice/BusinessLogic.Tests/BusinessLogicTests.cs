﻿using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.Abstractions;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogic.Tests
{
    [TestClass]
    public class TrendsBusinessLogicTests
    {
        private ITrendBusinessLogic _TrendBusinessLogic;
        private Mock<IRepository<Trend>> _trendRepositoryMock;

        public TrendsBusinessLogicTests()
        {
            _trendRepositoryMock = new Mock<IRepository<Trend>>();
            _TrendBusinessLogic = new TrendBusinessLogic(_trendRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsTheOnlyInstanceCreated()
        {
            //arrange
            TrendDto newTrendDto = new TrendDto { Id = Guid.NewGuid(), Name = "sport" };
            Trend newTrend = new Trend { Id = (Guid)newTrendDto.Id, Name = "sport" };
            ICollection<Trend> allTrends = new List<Trend>();
            allTrends.Add(newTrend);
            ICollection<TrendDto> allTrendDto = new List<TrendDto>();

            _trendRepositoryMock.Setup(x => x.GetAll()).Returns(allTrends);

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
            TrendDto newTrendDto = new TrendDto { Id = Guid.NewGuid(), Name = "sport" };
            Trend newTrend = new Trend { Id = (Guid)newTrendDto.Id, Name = "sport" };
            ICollection<Trend> allTrends = new List<Trend>();
            allTrends.Add(newTrend);
            ICollection<TrendDto> allTrendDto = new List<TrendDto>();

            _trendRepositoryMock.Setup(x => x.GetAll()).Returns(allTrends);

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
            TrendDto newTrendDto = new TrendDto { Id = Guid.NewGuid(), Name="sport"};
            Trend newTrend = new Trend { Id = (Guid)newTrendDto.Id, Name = "sport" };

            _trendRepositoryMock.Setup(x => x.GetById((Guid)newTrendDto.Id)).Returns(newTrend);

            //act
            TrendDto trendReturned = _TrendBusinessLogic.GetById((Guid)newTrendDto.Id);

            //assert
            Assert.AreEqual(newTrendDto.Id, trendReturned.Id);
            Assert.AreEqual(newTrendDto.Name, trendReturned.Name);
        }

        [TestMethod]
        public void Update_ChangesTrendData()
        {
            
        }
    }
}
