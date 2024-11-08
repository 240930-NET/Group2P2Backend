using Xunit;
using Moq;
using MoviesP2.API.Services;
using MoviesP2.Data.Repos;
using MoviesP2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesP2.Tests.Services
{
    public class WatchlistServiceTests
    {
        private readonly WatchlistService _watchlistService;
        private readonly Mock<IWatchlistRepo> _mockWatchlistRepo = new Mock<IWatchlistRepo>();

        public WatchlistServiceTests()
        {
            _mockWatchlistRepo = new Mock<IWatchlistRepo>();
            _watchlistService = new WatchlistService(_mockWatchlistRepo.Object);
        }

        [Fact]
        public void GetAllWatchlists_ShouldReturnListOfWatchlists()
        {
            // Arrange
            var mockWatchlists = new List<Watchlist>
            {
                new Watchlist { WatchlistId = 1, UserId = 1 },
                new Watchlist { WatchlistId = 2, UserId = 2 }
            };
            _mockWatchlistRepo.Setup(repo => repo.GetAllWatchlists()).Returns(mockWatchlists);

            // Act
            var result = _watchlistService.GetAllWatchlists();

            // Assert
            Assert.Equal(2, result.Count);
            _mockWatchlistRepo.Verify(repo => repo.GetAllWatchlists(), Times.Once);
        }

    [Fact]
        public void GetWatchlistByUserAuthId_ShouldReturnWatchlist()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1", Watchlist = new Watchlist { WatchlistId = 1, UserId = 1 }},
                new User { UserId = 2, AuthId = "auth2", Watchlist = new Watchlist { WatchlistId = 2, UserId = 2 }},
                new User { UserId = 3, AuthId = "auth3", Watchlist = new Watchlist { WatchlistId = 5, UserId = 3 }}
            };

            _mockWatchlistRepo.Setup(repo => repo.GetWatchlistByUserAuthId(It.IsAny<string>()))
                    .Returns((string AuthId) => mockUsers.FirstOrDefault(u => u.AuthId.Equals(AuthId))!.Watchlist);

            // Act
            var targetUser = new User { UserId = 3, AuthId = "auth3", Watchlist = new Watchlist { WatchlistId = 5, UserId = 3 }};
            var result = _watchlistService.GetWatchlistByUserAuthId(targetUser.AuthId);

            // Assert
            Assert.Equal(5, result.WatchlistId);
            _mockWatchlistRepo.Verify(repo => repo.GetWatchlistByUserAuthId(It.IsAny<string>()), Times.Once);
        }

    }

}