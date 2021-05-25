using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using Models.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests
{
    public class UserServiceTests : BaseBusinessLogicTests
    {
        private readonly Mock<UserManager<ApplicationUser>> userManagerMock;
        
        private readonly Mock<RoleManager<ModeratorRole>> moderatorRoleMock;
        
        private readonly IUserService systemUnderTest;

        private static readonly IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        public static Mock<UserManager<TIDentityUser>> GetUserManagerMock<TIDentityUser>() where TIDentityUser : ApplicationUser
        {
            return new Mock<UserManager<TIDentityUser>>(
                    new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    new IUserValidator<TIDentityUser>[0],
                    new IPasswordValidator<TIDentityUser>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object);
        }
        public static Mock<RoleManager<TIdentityRole>> GetRoleManagerMock<TIdentityRole>() where TIdentityRole : ModeratorRole
        {
            return new Mock<RoleManager<TIdentityRole>>(
                    new Mock<IRoleStore<TIdentityRole>>().Object,
                    new IRoleValidator<TIdentityRole>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<ILogger<RoleManager<TIdentityRole>>>().Object);
        }

        
        public UserServiceTests() : base()
        {
            userManagerMock = GetUserManagerMock<ApplicationUser>();
            moderatorRoleMock = GetRoleManagerMock<ModeratorRole>();
            systemUnderTest = new UserService(userManagerMock.Object, moderatorRoleMock.Object, mapper);
        }

        [Fact]
        public void Create_CheckAfterByEmail()
        {
            UserCreateDto userCreateDto = new UserCreateDto
            {
                Email = "TestUser@icanhaz.com",
                Password = "@PPaarrolaaa123",
                Username = "TestUser"
            };

            systemUnderTest.Create(userCreateDto);


            userManagerMock.Verify(x => x.FindByEmailAsync(userCreateDto.Email));
        }
        
        [Fact]
        public async void Create_ReturnNullIfUserAlreadyExist()
        {
            UserCreateDto userCreateDto = new UserCreateDto
            {
                Email = "TestUser@icanhaz.com",
                Password = "@PPaarrolaaa123",
                Username = "TestUser"
            };
            ApplicationUser existingUser = new ApplicationUser
            {
                Email = "TestUser@icanhaz.com"
            };
            UserGetAllDto userGetAllDto = new UserGetAllDto();
            userManagerMock.Setup(x => x.FindByEmailAsync(userCreateDto.Email)).Returns(Task.FromResult(existingUser));


            userGetAllDto = await systemUnderTest.Create(userCreateDto);


            Assert.Null(userGetAllDto);
        }

        [Fact]
        public async void Create_ReturnNullIfUserFailedToCreate()
        {
            UserCreateDto userCreateDto = new UserCreateDto
            {
                Email = "TestUser@icanhaz.com",
                Password = "@PPaarrolaaa123",
                Username = "TestUser"
            };
            UserGetAllDto userGetAllDto = new UserGetAllDto();
            ApplicationUser applicationUser = new ApplicationUser();
            userManagerMock.Setup(x => x.FindByEmailAsync(userCreateDto.Email)).Returns(Task.FromResult(new ApplicationUser()));
            userManagerMock.Setup(x => x.CreateAsync(applicationUser,userCreateDto.Password)).Returns(Task.FromResult<IdentityResult>(null));


            userGetAllDto = await systemUnderTest.Create(userCreateDto);


            Assert.Null(userGetAllDto);
        }

        [Fact]
        public async void GetById_DoseNotRetunrNullIfUserExist()
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };
            userManagerMock.Setup(x => x.FindByIdAsync("3fa85f64-5717-4562-b3fc-2c963f66afa6")).Returns(Task.FromResult(applicationUser));


            UserGetByIdDto userGetByIdDto = await systemUnderTest.GetById(applicationUser.Id);


            Assert.NotNull(userGetByIdDto);
        }
         
        [Fact]
        public async void GetById_RetunrNullUserifUSerDoseNotExist()
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            userManagerMock.Setup(x => x.FindByIdAsync(applicationUser.Id.ToString())).Returns(Task.FromResult<ApplicationUser>(new ApplicationUser()));


            UserGetByIdDto userGetByIdDto = await systemUnderTest.GetById(applicationUser.Id);


            Assert.Null(userGetByIdDto.Email);
            Assert.Null(userGetByIdDto.Username);
            Assert.Empty(userGetByIdDto.ModeratedTrendsIds);
        }

 
    }
}
