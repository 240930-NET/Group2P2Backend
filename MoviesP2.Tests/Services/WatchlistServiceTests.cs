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
       // public void GetWatchListByUserAuthId_ShouldReturnWatchlist_WhenExists()
       // {
            // Arrange
           // var watchlist = new Watchlist { WatchlistId = 1, UserId = 1 };
          //  _mockWatchlistRepo.Setup(static repo => repo.GetWatchListByUserAuthId("auth1")).ReturnsAsync(watchlist);

            // Act
         //   var result = _watchlistService.GetWatchListByUserAuthId("auth1");

            // Assert
          //  Assert.Equal(watchlist, result);
          // _mockWatchlistRepo.Verify(repo => repo.GetWatchListByUserAuthId("auth1"), Times.Once);
       // }

        //[Fact]
        public void AddMovieToWatchlist_ShouldCallRepoAddMovie()
        {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Movie 1" };

            // Act
            _watchlistService.AddMovieToWatchlist("auth1", movie);

            // Assert
            _mockWatchlistRepo.Verify(repo => repo.AddMovieToWatchlist(It.IsAny<int>(), movie), Times.Once);
        }

        [Fact]
        public void RemoveMovieFromWatchlist_ShouldCallRepoRemoveMovie()
        {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Movie 1" };

            // Act
            _watchlistService.RemoveMovieFromWatchlist("auth1", movie);

            // Assert
            _mockWatchlistRepo.Verify(repo => repo.RemoveMovieFromWatchlist(It.IsAny<int>(), movie), Times.Once);
        }
    }
}
