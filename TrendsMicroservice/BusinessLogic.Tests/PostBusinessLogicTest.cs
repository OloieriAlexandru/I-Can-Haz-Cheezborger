using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.Abstractions;
using Entities;
using Models;
using Models.Posts;
using Moq;
using System;
using Xunit;

namespace BusinessLogic.Tests
{
    public class PostBusinessLogicTest : BaseBusinessLogicTests
    {
        private readonly Mock<IRepository<Post>> postRepositoryMock;
        private readonly Mock<IRepository<PostReact>> postReactRepositoryMock;

        private readonly IPostBusinessLogic systemUnderTest;

        public PostBusinessLogicTest() : base()
        {
            postRepositoryMock = new Mock<IRepository<Post>>();
            postReactRepositoryMock = new Mock<IRepository<PostReact>>();
            systemUnderTest = new PostBusinessLogic(postRepositoryMock.Object, postReactRepositoryMock.Object, mapper);
        }

        [Fact]
        public void GetById_ReturnsNullIfPostDoesNotExist()
        {
            //Arrange
            postRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "Reacts")).Returns(() => null);
            PostGetByIdDto postGetById = new PostGetByIdDto();
            UserInfoModel userInfo = new UserInfoModel();

            //Act
            postGetById = systemUnderTest.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), userInfo);

            //Assert
            Assert.Null(postGetById);
        }

        [Fact]
        public void GetById_ReturnsDoesNotReturnNullIfPostExists()
        {
            //Arrange
            PostGetByIdDto postGetById = new PostGetByIdDto();
            UserInfoModel userInfo = new UserInfoModel();
            Post post = new Post();
            postRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "Reacts")).Returns(post);
            

            //Act
            postGetById = systemUnderTest.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), userInfo);

            //Assert
            Assert.NotNull(postGetById);
        }

        [Fact]
        public void GetById_ReturnsPostWithThatId()
        {
            //Arrange
            PostGetByIdDto postGetById = new PostGetByIdDto();
            UserInfoModel userInfo = new UserInfoModel();
            Post post = new Post();
            post.Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            postRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "Reacts")).Returns(post);
            PostGetByIdDto mappedPost = mapper.Map<PostGetByIdDto>(post);


            //Act
            postGetById = systemUnderTest.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), userInfo);

            //Assert
            Assert.True(postGetById.Id.Equals(mappedPost.Id));
        }

        [Fact]
        public void Create_SavesChangesAfterInsert()
        {
            //Arrange
            PostCreateDto postCreateDto = new PostCreateDto();
            Post mappedPost = mapper.Map<Post>(postCreateDto);

            //Act
            systemUnderTest.Create(postCreateDto);

            //Assert
            postRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Patch_UpdatesRepoWithPostGiven()
        {
            //Arrange
            PostPatchDto postPatch = new PostPatchDto();
            postPatch.Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            Post post = mapper.Map<Post>(postPatch);
            postRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))).Returns(post);

            //Act
            systemUnderTest.Patch(postPatch);

            //Assert
            postRepositoryMock.Verify(m => m.Update(post), Times.Once);
        }

        [Fact]
        public void Patch_ReturnsNullIfPostGivenDoesNotExist()
        {
            //Arrange
            PostPatchDto postPatch = new PostPatchDto();
            postPatch.Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            Post post = mapper.Map<Post>(postPatch);
            postRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))).Returns(() => null);

            //Act
            systemUnderTest.Patch(postPatch);

            //Assert
            postRepositoryMock.Verify(m => m.Update(post), Times.Never);
            postRepositoryMock.Verify(m => m.SaveChanges(), Times.Never);
        }

        [Fact]
        public void Patch_SavesRepoAfterUpdate()
        {
            //Arrange
            PostPatchDto postPatch = new PostPatchDto();
            postPatch.Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            Post post = mapper.Map<Post>(postPatch);
            postRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))).Returns(post);

            //Act
            systemUnderTest.Patch(postPatch);

            //Assert
            postRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_CallsDeletePostFromRepo()
        {
            //Act
            systemUnderTest.Delete(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            //Assert
            postRepositoryMock.Verify(m => m.Delete(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")), Times.Once);
        }

        [Fact]
        public void Delete_SavesChangesAfterDelete()
        {
            //Act
            systemUnderTest.Delete(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            //Assert
            postRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
