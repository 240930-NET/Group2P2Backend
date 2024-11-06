using Xunit;
using Moq;
using MoviesP2.API.Services;
using MoviesP2.Data.Repos;
using MoviesP2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesP2.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepo> _mockUserRepo;

        public UserServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepo>();
            _userService = new UserService(_mockUserRepo.Object);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnListOfUsers()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1" },
                new User { UserId = 2, AuthId = "auth2" }
            };
            _mockUserRepo.Setup(repo => repo.GetAllUsers()).ReturnsAsync(mockUsers);

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.Equal(2, result.Count);
            _mockUserRepo.Verify(repo => repo.GetAllUsers(), Times.Once);
        }

        [Fact]
        public async Task GetUserByAuthId_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { UserId = 1, AuthId = "auth1" };
            _mockUserRepo.Setup(repo => repo.GetUserByAuthId("auth1")).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByAuthId("auth1");

            // Assert
            Assert.Equal(user, result);
            _mockUserRepo.Verify(repo => repo.GetUserByAuthId("auth1"), Times.Once);
        }

        [Fact]
        public async Task AddUser_ShouldAddUser()
        {
            // Arrange
            var user = new User { AuthId = "auth3" };
            _mockUserRepo.Setup(repo => repo.AddUser(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.AddUser("auth3");

            // Assert
            Assert.Equal(user, result);
            _mockUserRepo.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
        }
    }
}
