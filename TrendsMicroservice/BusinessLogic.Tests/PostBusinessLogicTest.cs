﻿using BusinessLogic.Abstractions;
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
    public class PostBusinessLogicTest : BaseBusinessLogicTests
    {
        private readonly Mock<IRepository<Post>> postRepositoryMock;

        private readonly IPostBusinessLogic systemUnderTest;

        private readonly Post testPost = new Post()
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Title = "TestPost",
            MediaPath = "TestPostMediaPath",
            TrendId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        private readonly PostGetByIdDto testPostGetByIdDto = new PostGetByIdDto()
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Title = "TestPost",
            MediaPath = "TestPostMediaPath",
            TrendId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        private readonly PostGetAllDto testPostGetAllDto = new PostGetAllDto()
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Title = "TestPost",
            MediaPath = "TestPostMediaPath",
            TrendId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        private readonly PostCreateDto testPostCreateDto = new PostCreateDto()
        {
            Title = "TestPost",
            MediaPath = "TestPostMediaPath",
            TrendId = "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        };

        public PostBusinessLogicTest() : base()
        {
            postRepositoryMock = new Mock<IRepository<Post>>();
            systemUnderTest = new PostBusinessLogic(postRepositoryMock.Object, mapper);
        }

        [TestMethod]
        public void GetAll_ReturnsAllTheInstances()
        {
            // Arrange
            ICollection<Post> posts = new List<Post> { testPost };
            postRepositoryMock.Setup(x => x.GetAll()).Returns(posts);
            ICollection<PostGetAllDto> expectedPosts = new List<PostGetAllDto> { testPostGetAllDto };

            // Act
            ICollection<PostGetAllDto> returnedPosts = systemUnderTest.GetAll(testPost.TrendId);

            // Assert
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expectedPosts, (System.Collections.ICollection)returnedPosts);
        }

        [TestMethod]
        public void GetById_ReturnsCreatedInstance()
        {
            //Arrange
            postRepositoryMock.Setup(x => x.GetById(testPost.Id)).Returns(testPost);

            //Act
            PostGetByIdDto returnedPost = systemUnderTest.GetById(testPost.Id);

            //Assert
            Assert.AreEqual(testPostGetByIdDto, returnedPost);
        }

        [TestMethod]
        public void Create_ReturnsCreatedInstance()
        { 
            // Act
            PostGetAllDto returnedPost = systemUnderTest.Create(testPostCreateDto);

            // Assert
            Assert.AreEqual(testPostGetAllDto.Title, returnedPost.Title);
            Assert.AreEqual(testPostGetAllDto.MediaPath, returnedPost.MediaPath);
            Assert.AreEqual(testPostGetAllDto.TrendId, returnedPost.TrendId);
        }
    }
}
