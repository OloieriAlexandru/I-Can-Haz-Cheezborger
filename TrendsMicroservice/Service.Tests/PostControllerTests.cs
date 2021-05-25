using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Posts;
using Moq;
using Service.Controllers;
using System;
using Xunit;

namespace Service.Tests
{
    public class PostControllerTests
    {
        private readonly Mock<IPostBusinessLogic> postBusinessLogicMock;

        private readonly PostController systemUnderTest;

        public PostControllerTests() : base()
        {
            postBusinessLogicMock = new Mock<IPostBusinessLogic>();
            systemUnderTest = new PostController(postBusinessLogicMock.Object);
        }

        [Fact]
        public void GetById_ReturnsTrendById()
        {
            //Arrange
            PostGetByIdDto post = new PostGetByIdDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
            postBusinessLogicMock.Setup(x => x.GetById(post.Id, null)).Returns(post);

            //Act
            // https://stackoverflow.com/questions/61453820/get-a-value-from-actionresultobject-in-a-asp-net-core-api-method
            var result = (OkObjectResult)systemUnderTest.GetByIdUnauthorized(post.Id).Result;
            PostGetByIdDto postGetByIdDto = (PostGetByIdDto)result.Value;

            //Assert
            Assert.True(post.Id.Equals(postGetByIdDto.Id));
            Assert.True(result.StatusCode.Equals(200));
        }

        [Fact]
        public void GetById_ReturnsNullIfPostDoesNotExist()
        {
            PostGetByIdDto post = new PostGetByIdDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
            postBusinessLogicMock.Setup(x => x.GetById(post.Id, null)).Returns(() => null);

            var result = (NotFoundResult)systemUnderTest.GetByIdUnauthorized(post.Id).Result;

            Assert.True(result.StatusCode.Equals(404));
        }

        [Fact]
        public void Patch_UpdatesPost()
        {
            PostPatchDto post = new PostPatchDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            systemUnderTest.Patch(post.Id, post);

            postBusinessLogicMock.Verify(m => m.Patch(post), Times.Once);
        }

        [Fact]
        public void Patch_ReturnsBadRequestIfPostDoesNotExist()
        {
            PostPatchDto post = new PostPatchDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            var result = (BadRequestResult)systemUnderTest.Patch(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"), post);

            Assert.True(result.StatusCode.Equals(400));
        }

        [Fact]
        public void Delete_CallsDeleteFromBusinessLogic()
        {
            Guid id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

            var result = (NoContentResult)systemUnderTest.Delete(id);

            postBusinessLogicMock.Verify(m => m.Delete(id), Times.Once);
            Assert.True(result.StatusCode.Equals(204));
        }
    }
}
