using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.Abstractions;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;

namespace BusinessLogic.Tests
{
    [TestClass]
    public class CommentBusinessLogicTests : BaseBusinessLogicTests
    {
        private readonly Mock<IRepository<Comment>> commentRepositoryMock;

        private readonly ICommentBusinessLogic systemUnderTest;

        private readonly Comment testComment = new Comment()
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Text = "TestCommentText",
            PostId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        private readonly CommentGetDto testCommentGetDto = new CommentGetDto()
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Text = "TestCommentText",
            PostId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        private readonly CommentUpdateDto testCommentUpdateDto = new CommentUpdateDto()
        {
            Id = Guid.Parse("3fa85f64-0000-4562-b3fc-2c963f66afa6"),
            Text = "TestCommentUpdateText",
            PostId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        private readonly CommentCreateDto testCommentCreateDto = new CommentCreateDto()
        {
            Text = "TestCommentText",
            PostId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        public CommentBusinessLogicTests() : base()
        {
            commentRepositoryMock = new Mock<IRepository<Comment>>();
            systemUnderTest = new CommentBusinessLogic(commentRepositoryMock.Object, mapper);
        }

        [TestMethod]
        public void GetById_ReturnsCreatedInstance()
        {
            //Arrange
            commentRepositoryMock.Setup(x => x.GetById(testComment.Id)).Returns(testComment);
           
            //Act
            CommentGetDto returnedTrend = systemUnderTest.GetById(testComment.Id);

            //Assert
            Assert.AreEqual(testCommentGetDto, returnedTrend);
        }

        [TestMethod]
        public void Create_ReturnsCreatedInstance()
        {

            //Act
            CommentGetDto returnedComment = systemUnderTest.Create(testCommentCreateDto);

            //Assert
            Assert.AreEqual(testCommentCreateDto, returnedComment);
        }

    }
}