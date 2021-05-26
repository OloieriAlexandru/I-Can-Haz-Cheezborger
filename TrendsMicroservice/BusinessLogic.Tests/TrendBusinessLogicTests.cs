using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.Abstractions;
using Entities;
using Models.Trends;
using Moq;
using System;
using Xunit;

namespace BusinessLogic.Tests
{
    public class TrendBusinessLogicTests : BaseBusinessLogicTests
    {
        private readonly Mock<IRepository<Trend>> trendRepositoryMock;

        private readonly Mock<IRepository<TrendFollow>> trendFollowRepositoryMock;

        private readonly Mock<IContentScanTaskService> contentScanTaskServiceMock;

        private readonly Mock<IImageService> imageServiceMock;

        private readonly ITrendBusinessLogic systemUnderTest;

        public TrendBusinessLogicTests() : base()
        {
            trendRepositoryMock = new Mock<IRepository<Trend>>();
            trendFollowRepositoryMock = new Mock<IRepository<TrendFollow>>();
            contentScanTaskServiceMock = new Mock<IContentScanTaskService>();
            imageServiceMock = new Mock<IImageService>();
            systemUnderTest = new TrendBusinessLogic(trendRepositoryMock.Object, trendFollowRepositoryMock.Object,
                mapper, contentScanTaskServiceMock.Object, imageServiceMock.Object);
        }

        [Fact]
        public void GetById_ReturnsTrendById()
        {
            //Arrange
            Trend trend = new Trend
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
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
            Trend trend = new Trend
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
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
            TrendCreateDto trendCreateDto = new TrendCreateDto
            {
                CreatorId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            //Act
            TrendGetAllDto trendGetAllDto = systemUnderTest.Create(trendCreateDto);

            //Assert
            Assert.NotNull(trendGetAllDto);
            Assert.True(trendGetAllDto.CreatorId.Equals(trendCreateDto.CreatorId));
        }

        // [Fact]
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

        [Fact]
        public void Update_UpdateCommentRepoWithThatComment()
        {
            TrendUpdateDto trendCUpdateDto = new TrendUpdateDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
            Trend trend = new Trend();
            mapper.Map<TrendUpdateDto, Trend>(trendCUpdateDto, trend);
            trendRepositoryMock.Setup(x => x.GetById(trendCUpdateDto.Id)).Returns(trend);


            systemUnderTest.Update(trendCUpdateDto);

            trendRepositoryMock.Verify(m => m.Update(trend), Times.Once);
        }

        [Fact]
        public void Update_SavesChangesAfterUpdate()
        {
            TrendUpdateDto trendCUpdateDto = new TrendUpdateDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
            Trend trend = new Trend();
            mapper.Map<TrendUpdateDto, Trend>(trendCUpdateDto, trend);
            trendRepositoryMock.Setup(x => x.GetById(trendCUpdateDto.Id)).Returns(trend);


            systemUnderTest.Update(trendCUpdateDto);

            trendRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_CallsDeleteFromRepo()
        {
            Guid id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

            systemUnderTest.Delete(id);

            trendRepositoryMock.Verify(m => m.Delete(id), Times.Once);
        }

        [Fact]
        public void Delete_SavesChangesAfterDelete()
        {
            Guid id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

            systemUnderTest.Delete(id);

            trendRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void PatchFollow_InsertsTrendFollowInRepoIfRequired()
        {
            TrendPatchFollowDto trendPatchFollowDto = new TrendPatchFollowDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                CreatorId = Guid.Parse("2fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Type = "Follow"
            };
            TrendFollow trendFollow = new TrendFollow
            {
                TrendId = trendPatchFollowDto.Id,
                UserId = trendPatchFollowDto.CreatorId
            };
            Trend trend = new Trend
            {
                FollowersCount = 0
            };
            trendFollowRepositoryMock.Setup(x => x.GetByFilter(tf => tf.UserId == trendPatchFollowDto.CreatorId && tf.TrendId == trendPatchFollowDto.Id)).Returns(() => null);
            trendRepositoryMock.Setup(x => x.GetById(trendPatchFollowDto.Id)).Returns(trend);

            systemUnderTest.PatchFollow(trendPatchFollowDto);
            trend.FollowersCount += 1;

            trendFollowRepositoryMock.Verify(m => m.Insert(It.IsAny<TrendFollow>()), Times.Once);
        }

        [Fact]
        public void PatchFollow_SavesChangesAfterInsert()
        {
            TrendPatchFollowDto trendPatchFollowDto = new TrendPatchFollowDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                CreatorId = Guid.Parse("2fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Type = "Follow"
            };
            TrendFollow trendFollow = new TrendFollow
            {
                TrendId = trendPatchFollowDto.Id,
                UserId = trendPatchFollowDto.CreatorId
            };
            Trend trend = new Trend
            {
                FollowersCount = 0
            };
            trendFollowRepositoryMock.Setup(x => x.GetByFilter(tf => tf.UserId == trendPatchFollowDto.CreatorId && tf.TrendId == trendPatchFollowDto.Id)).Returns(() => null);
            trendRepositoryMock.Setup(x => x.GetById(trendPatchFollowDto.Id)).Returns(trend);

            systemUnderTest.PatchFollow(trendPatchFollowDto);
            trend.FollowersCount += 1;

            trendFollowRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void PatchFollow_DeletesFollowInRepoIfRequired()
        {
            TrendPatchFollowDto trendPatchFollowDto = new TrendPatchFollowDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                CreatorId = Guid.Parse("2fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Type = "Not Follow"
            };
            TrendFollow trendFollow = new TrendFollow
            {
                TrendId = trendPatchFollowDto.Id,
                UserId = trendPatchFollowDto.CreatorId
            };
            Trend trend = new Trend
            {
                FollowersCount = 0
            };
            trendFollowRepositoryMock.Setup(x => x.GetByFilter(tf => tf.UserId == trendPatchFollowDto.CreatorId && tf.TrendId == trendPatchFollowDto.Id)).Returns(trendFollow);
            trendRepositoryMock.Setup(x => x.GetById(trendPatchFollowDto.Id)).Returns(trend);

            systemUnderTest.PatchFollow(trendPatchFollowDto);
            trend.FollowersCount += 1;

            trendFollowRepositoryMock.Verify(m => m.Delete(trendFollow.Id), Times.Once);
        }

        [Fact]
        public void PatchFollow_TrendFollowCountIncreasesAfterInsert()
        {
            TrendPatchFollowDto trendPatchFollowDto = new TrendPatchFollowDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                CreatorId = Guid.Parse("2fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Type = "Follow"
            };
            TrendFollow trendFollow = new TrendFollow
            {
                TrendId = trendPatchFollowDto.Id,
                UserId = trendPatchFollowDto.CreatorId
            };
            Trend trend = new Trend
            {
                FollowersCount = 0
            };
            trendFollowRepositoryMock.Setup(x => x.GetByFilter(tf => tf.UserId == trendPatchFollowDto.CreatorId && tf.TrendId == trendPatchFollowDto.Id)).Returns(() => null);
            trendRepositoryMock.Setup(x => x.GetById(trendPatchFollowDto.Id)).Returns(trend);

            systemUnderTest.PatchFollow(trendPatchFollowDto);
            trend.FollowersCount += 1;

            trendRepositoryMock.Verify(m => m.Update(trend), Times.Once);
        }

        [Fact]
        public void PatchFollow_TrendFollowCountDecreasesAfterFollowDelete()
        {
            TrendPatchFollowDto trendPatchFollowDto = new TrendPatchFollowDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                CreatorId = Guid.Parse("2fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Type = "Not Follow"
            };
            TrendFollow trendFollow = new TrendFollow
            {
                TrendId = trendPatchFollowDto.Id,
                UserId = trendPatchFollowDto.CreatorId
            };
            Trend trend = new Trend
            {
                FollowersCount = 10
            };
            trendFollowRepositoryMock.Setup(x => x.GetByFilter(tf => tf.UserId == trendPatchFollowDto.CreatorId && tf.TrendId == trendPatchFollowDto.Id)).Returns(trendFollow);
            trendRepositoryMock.Setup(x => x.GetById(trendPatchFollowDto.Id)).Returns(trend);

            systemUnderTest.PatchFollow(trendPatchFollowDto);
            trend.FollowersCount -= 1;

            trendRepositoryMock.Verify(m => m.Update(trend), Times.Once);
        }

        [Fact]
        public void PatchFollow_SavesChangesInTrend()
        {
            TrendPatchFollowDto trendPatchFollowDto = new TrendPatchFollowDto
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                CreatorId = Guid.Parse("2fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Type = "Not Follow"
            };
            TrendFollow trendFollow = new TrendFollow
            {
                TrendId = trendPatchFollowDto.Id,
                UserId = trendPatchFollowDto.CreatorId
            };
            Trend trend = new Trend
            {
                FollowersCount = 10
            };
            trendFollowRepositoryMock.Setup(x => x.GetByFilter(tf => tf.UserId == trendPatchFollowDto.CreatorId && tf.TrendId == trendPatchFollowDto.Id)).Returns(trendFollow);
            trendRepositoryMock.Setup(x => x.GetById(trendPatchFollowDto.Id)).Returns(trend);

            systemUnderTest.PatchFollow(trendPatchFollowDto);
            trend.FollowersCount -= 1;

            trendRepositoryMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
