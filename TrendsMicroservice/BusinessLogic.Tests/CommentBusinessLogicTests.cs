using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.Abstractions;
using Entities;
using Models.Comments;
using Moq;
using System;
using Xunit;

namespace BusinessLogic.Tests
{
    public class CommentBusinessLogicTests : BaseBusinessLogicTests
    {
        private readonly Mock<IRepository<Comment>> commentRepositoryMock;

        private readonly Mock<IRepository<Post>> postRepositoryMock;
        
        private readonly Mock<IRepository<CommentReact>> commentReactRepositoryMock;
        
        private readonly Mock<IContentScanTaskService> contentScanTaskServiceMock;
        
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

        private readonly CommentCreateDto testCommentCreateDto = new CommentCreateDto()
        {
            Text = "TestCommentText",
            PostId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        private readonly CommentPatchDto testCommentPatchDto = new CommentPatchDto()
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Text = "New text"
        };

        public CommentBusinessLogicTests() : base()
        {
            commentRepositoryMock = new Mock<IRepository<Comment>>();
            postRepositoryMock = new Mock<IRepository<Post>>();
            commentReactRepositoryMock = new Mock<IRepository<CommentReact>>();
            contentScanTaskServiceMock = new Mock<IContentScanTaskService>();
            systemUnderTest = new CommentBusinessLogic(postRepositoryMock.Object, commentRepositoryMock.Object,
                commentReactRepositoryMock.Object, mapper, contentScanTaskServiceMock.Object);
        }

        [Fact]
        public void GetById_ReturnsCreatedInstance()
        {
            //Arrange
            commentRepositoryMock.Setup(x => x.GetById(testComment.Id)).Returns(testComment);

            //Act
            CommentGetDto returnedComment = systemUnderTest.GetById(testComment.Id);

            //Assert
            Assert.True(testCommentGetDto.Id.Equals(returnedComment.Id));
            Assert.True(testCommentGetDto.PostId.Equals(returnedComment.PostId));
            Assert.True(testCommentGetDto.Text.Equals(returnedComment.Text));
        }

        [Fact]
        public void UpdateComment_UpdateCommentRepoWithThatComment()
        {
            //Arrange
            Comment updatedComment = new Comment();
            mapper.Map<CommentPatchDto, Comment>(testCommentPatchDto, updatedComment);
            commentRepositoryMock.Setup(x => x.GetById(testCommentPatchDto.Id)).Returns(updatedComment);

            //Act
            systemUnderTest.Patch(testCommentPatchDto);

            //Assert
            commentRepositoryMock.Verify(m => m.Update(updatedComment), Times.Once);
        }

        [Fact]
        public void UpdateComment_SavesChangesAfterRepoUpdate()
        {
            //Arrange
            Comment updatedComment = new Comment();
            mapper.Map<CommentPatchDto, Comment>(testCommentPatchDto, updatedComment);
            commentRepositoryMock.Setup(x => x.GetById(testCommentPatchDto.Id)).Returns(updatedComment);

            //Act
            systemUnderTest.Patch(testCommentPatchDto);

            //Assert
            commentRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteComment_DeletesCommentFromRepo()
        {
            //Arrange
            Comment comment = new Comment();
            Post post = new Post();
            mapper.Map<CommentPatchDto, Comment>(testCommentPatchDto, comment);
            postRepositoryMock.Setup(x => x.GetById(comment.PostId)).Returns(post);
            commentRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))).Returns(comment);

            //Act
            systemUnderTest.Delete(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            //Assert
            commentRepositoryMock.Verify(m => m.Delete(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")), Times.Once);
        }

        [Fact]
        public void DeleteComment_SavesChangesAfterDelete()
        {
            //Arrange
            Comment comment = new Comment();
            Post post = new Post();
            mapper.Map<CommentPatchDto, Comment>(testCommentPatchDto, comment);
            postRepositoryMock.Setup(x => x.GetById(comment.PostId)).Returns(post);
            commentRepositoryMock.Setup(x => x.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))).Returns(comment);

            //Act
            systemUnderTest.Delete(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            //Assert
            commentRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}