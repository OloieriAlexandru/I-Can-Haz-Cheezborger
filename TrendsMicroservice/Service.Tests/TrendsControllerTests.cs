using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Trends;
using Moq;
using Service.Controllers;
using System;
using Xunit;

namespace Service.Tests
{
    public class TrendsControllerTests
    {

        private readonly Mock<ITrendBusinessLogic> trendBusinessLogicMock;

        private readonly TrendController systemUnderTest;


        public TrendsControllerTests() : base()
        {
            trendBusinessLogicMock = new Mock<ITrendBusinessLogic>();
            systemUnderTest = new TrendController(trendBusinessLogicMock.Object);
        }

        [Fact]
        public void GetById_ReturnsTrendById()
        {
            //Arrange
            TrendGetByIdDto trend = new TrendGetByIdDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
            trendBusinessLogicMock.Setup(x => x.GetById(trend.Id)).Returns(trend);

            //Act
            // https://stackoverflow.com/questions/61453820/get-a-value-from-actionresultobject-in-a-asp-net-core-api-method
            var result = (OkObjectResult)systemUnderTest.GetById(trend.Id).Result;
            TrendGetByIdDto trendGetByIdDto = (TrendGetByIdDto)result.Value;

            //Assert
            Assert.True(trend.Id.Equals(trendGetByIdDto.Id));
            Assert.True(result.StatusCode.Equals(200));
        }

        [Fact]
        public void GetById_ReturnsNotFoundIfTrendDoesNotExist()
        {
            TrendGetByIdDto trend = new TrendGetByIdDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
            trendBusinessLogicMock.Setup(x => x.GetById(trend.Id)).Returns(() => null);

            var result = (NotFoundResult)systemUnderTest.GetById(trend.Id).Result;

            Assert.True(result.StatusCode.Equals(404));
        }

        [Fact]
        public void Update_CallsUpdateFromBusinessLogic()
        {
            TrendUpdateDto trend = new TrendUpdateDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            systemUnderTest.Update(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), trend);

            trendBusinessLogicMock.Verify(m => m.Update(trend), Times.Once);
        }

        [Fact]
        public void Update_ReturnsBadRequestIfTrendDoesNotExist()
        {
            TrendUpdateDto trend = new TrendUpdateDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            systemUnderTest.Update(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8"), trend);
            var result = (BadRequestResult)systemUnderTest.Update(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8"), trend);

            Assert.True(result.StatusCode.Equals(400));
        }

        [Fact]
        public void Delete_CallsDeleteFromBusinessLogic()
        {
            Guid id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

            var result = (NoContentResult)systemUnderTest.Delete(id);

            trendBusinessLogicMock.Verify(m => m.Delete(id), Times.Once);
            Assert.True(result.StatusCode.Equals(204));
        }
    }
}
