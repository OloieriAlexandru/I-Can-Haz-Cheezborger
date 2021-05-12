using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.Abstractions;
using Entities;
using Models.Trends;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessLogic.Tests
{
    public class TrendBusinessLogicTests : BaseBusinessLogicTests
    {
        private readonly Mock<IRepository<Trend>> trendRepositoryMock;

        private readonly Mock<IRepository<TrendFollow>> trendFollowRepositoryMock;

        private readonly ITrendBusinessLogic systemUnderTest;

        public TrendBusinessLogicTests() : base()
        {
            trendRepositoryMock = new Mock<IRepository<Trend>>();
            trendFollowRepositoryMock = new Mock<IRepository<TrendFollow>>();
            systemUnderTest = new TrendBusinessLogic(trendRepositoryMock.Object, trendFollowRepositoryMock.Object, mapper);
        }

        [Fact]
        public void GetById_ReturnsTrendById()
        {
            //Arrange
            Trend trend = new Trend();
            trend.Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            trendRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))).Returns(trend);

            //Act
            TrendGetByIdDto trendGetByIdDto = systemUnderTest.GetById(trend.Id);

            //Assert
            Assert.True(trend.Id.Equals(trendGetByIdDto.Id));
        }

        [Fact]
        public void GetById_ReturnsNullIfTrendDoesNotExist()
        {
            //Arrange
            Trend trend = new Trend();
            trend.Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            trendRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))).Returns(() => null);

            //Act
            TrendGetByIdDto trendGetByIdDto = systemUnderTest.GetById(trend.Id);

            //Assert
            Assert.Null(trendGetByIdDto);
        }

        [Fact]
        public void Create_InsertsTheTrendGiven()
        {
            //Arrange
            TrendCreateDto trendCreateDto = new TrendCreateDto();
            trendCreateDto.CreatorId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            Trend trend = mapper.Map<Trend>(trendCreateDto);

            //Act
            TrendGetAllDto trendGetAllDto = systemUnderTest.Create(trendCreateDto);

            //Assert
            Assert.True(trendGetAllDto.CreatorId.Equals(trendCreateDto.CreatorId));
        }

        [Fact]
        public void Create_SavesChangesAfterCreate()
        {
            //Arrange
            TrendCreateDto trendCreateDto = new TrendCreateDto();
            Trend trend = mapper.Map<Trend>(trendCreateDto);

            //Act
            systemUnderTest.Create(trendCreateDto);

            //Assert
            trendRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
