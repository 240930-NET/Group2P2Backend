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
                new User { UserId = 2, AuthId = "auth2" },
                new User { UserId = 3, AuthId = "auth3" }
            };
            _mockUserRepo.Setup(repo => repo.GetAllUsers()).ReturnsAsync(mockUsers);

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.Equal(3, result.Count);
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
        public async Task GetUserWatchlist_ShouldReturnWatchlist_WhenWatchlistExists()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1", Watchlist = new Watchlist { WatchlistId = 1, UserId = 1 }},
                new User { UserId = 2, AuthId = "auth2", Watchlist = new Watchlist { WatchlistId = 2, UserId = 2 }},
                new User { UserId = 3, AuthId = "auth3", Watchlist = new Watchlist { WatchlistId = 4, UserId = 3, }}
            };

            var mockWatchlist = new List<Watchlist>
            {
                new Watchlist { WatchlistId = 1, UserId = 1, Movies={new Movie{MovieId=1, Title="Action Movie"}, new Movie{MovieId=2, Title="Comic Movie"}}},
                new Watchlist { WatchlistId = 2, UserId = 2, Movies={}},
                new Watchlist { WatchlistId = 4, UserId = 3, Movies={new Movie{MovieId=2, Title="Comic Movie"}}},
            };

            List<Movie> expected = [new Movie{MovieId=2, Title="Comic Movie"}];

            // Repo : GetUserWatchlist
            _mockUserRepo.Setup(repo => repo.GetUserWatchlist(It.IsAny<string>()))
                   .ReturnsAsync((string authId) => mockWatchlist
                        .FirstOrDefault(w => Equals(w.UserId, 
                            mockUsers.FirstOrDefault(u => Equals(u.AuthId, authId))!.UserId))!.Movies);
                    
            
            // Act
            List<Movie> result = await _userService.GetUserWatchlist("auth3");

            // Assert
            Assert.Equal(expected[0].Title, result[0].Title);
            _mockUserRepo.Verify(repo => repo.GetUserWatchlist(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetUserWatchedMovies_ShouldReturnWatchedMovies_WhenWatchedMoviesExist()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1", Movies={new Movie{MovieId=1, Title="Action Movie"}, new Movie{MovieId=2, Title="Comic Movie"}}},
                new User { UserId = 2, AuthId = "auth2", Movies={}},
                new User { UserId = 3, AuthId = "auth3", Movies={new Movie{MovieId=1, Title="Action Movie"}, new Movie{MovieId=3, Title="Animation"}}}
            };

            // Repo : GetUserWatchlist
            _mockUserRepo.Setup(repo => repo.GetUserWatchedMovies(It.IsAny<string>()))
                   .ReturnsAsync((string authId) => mockUsers.FirstOrDefault(m => Equals(m.AuthId, authId))!.Movies);
            
            // Act
            List<Movie> result = await _userService.GetUserWatchedMovies("auth3");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, m => m.Title.Equals("Animation"));
            _mockUserRepo.Verify(repo => repo.GetUserWatchedMovies(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task AddUser_ShouldAddUser()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1" },
                new User { UserId = 2, AuthId = "auth2" },
                new User { UserId = 3, AuthId = "auth3" }
            };

            var newUser = new User { UserId = 4, AuthId = "auth4" };
           
            // Repo : GetUserByAuthId
            _mockUserRepo.Setup(repo => repo.GetUserByAuthId(It.IsAny<string>()))
                  .ReturnsAsync((string authId) => mockUsers.FirstOrDefault(u => u.AuthId == authId));

            // Repo : AddUser
            _mockUserRepo.Setup(repo => repo.AddUser(It.IsAny<User>()))
                  .Callback((User user) => mockUsers.Add(user));

            // Act
            var result = await _userService.AddUser(newUser.AuthId);

            // Assert
            Assert.Equal(4, mockUsers.Count);
            _mockUserRepo.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_ShouldDeleteUser()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1" },
                new User { UserId = 2, AuthId = "auth2" },
                new User { UserId = 3, AuthId = "auth3" }
            };

            var targetUser = new User { UserId = 2, AuthId = "auth2" };
           
            // Repo : GetUserByAuthId
            _mockUserRepo.Setup(repo => repo.GetUserByAuthId(It.IsAny<string>()))
                  .ReturnsAsync((string authId) => mockUsers.FirstOrDefault(u => u.AuthId == authId));

            // Repo : DeleteUser
            _mockUserRepo.Setup(repo => repo.DeleteUser(It.IsAny<User>()))
                  .Callback((User user) => mockUsers.Remove(user));

            // Act
            var result = await _userService.DeleteUser(targetUser.AuthId);

            // Assert
            Assert.Equal(2, mockUsers.Count);
            _mockUserRepo.Verify(repo => repo.DeleteUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task AddWatchedMovie_ShouldAddWatchedMovie_ToExistUser()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1", Movies={new Movie{MovieId=1, Title="Action Movie"}, new Movie{MovieId=2, Title="Comic Movie"}}},
                new User { UserId = 2, AuthId = "auth2", Movies={}},
                new User { UserId = 3, AuthId = "auth3", Movies={new Movie{MovieId=1, Title="Action Movie"}}}
            };

            Movie newMovie = new Movie{MovieId=3, Title="Animation"};

            // Repo : AddMovieToWatchedMovies
            _mockUserRepo.Setup(repo => repo.AddMovieToWatchedMovies(It.IsAny<string>(), It.IsAny<Movie>()))
                   .Callback((string authId, Movie movie) => mockUsers.FirstOrDefault(m => Equals(m.AuthId, authId))!.Movies.Add(movie));
            
            // Act
            _ = await _userService.AddWatchedMovie("auth3", newMovie);
            var user = mockUsers.FirstOrDefault(u => u.AuthId.Equals("auth3"));
            // Assert
            Assert.Contains(user!.Movies, m => m.Title.Equals(newMovie.Title));
            _mockUserRepo.Verify(repo => repo.AddMovieToWatchedMovies(It.IsAny<string>(), It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public async Task RemoveWatchedMovie_ShouldRemoveWatchedMovie_FromExistUser()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1", Movies={new Movie{MovieId=1, Title="Action Movie", ReleaseYear=2000}, new Movie{MovieId=2, Title="Comic Movie", ReleaseYear=2020 }}},
                new User { UserId = 2, AuthId = "auth2", Movies={}},
                new User { UserId = 3, AuthId = "auth3", Movies={new Movie{MovieId=1, Title="Action Movie", ReleaseYear=2000}}}
            };

            Movie targetMovie = new Movie{MovieId=1, Title="Action Movie", ReleaseYear=2000};

            // Repo : RemoveWatchedMovie
            _mockUserRepo.Setup(repo => repo.RemoveMovieFromWatchedMovies(It.IsAny<string>(), It.IsAny<Movie>()))
                   .Callback((string authId, Movie movie) => mockUsers.FirstOrDefault(m => Equals(m.AuthId, authId))!.Movies.RemoveAll(m => m.MovieId == movie.MovieId));
            
            // Act
            _ = await _userService.RemoveWatchedMovie("auth1", targetMovie);
            var user = mockUsers.FirstOrDefault(u => u.AuthId.Equals("auth1"));
            // Assert
            Assert.DoesNotContain(user!.Movies, m => m.Title.Equals(targetMovie.Title));
            Assert.Single(user.Movies);
            _mockUserRepo.Verify(repo => repo.RemoveMovieFromWatchedMovies(It.IsAny<string>(), It.IsAny<Movie>()), Times.Once);
        }
         

        [Fact]
        public async Task AddMovieToWatchlist_ShouldAddMovie_ToWatchList_ToExistUser()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1", Watchlist = new Watchlist { WatchlistId = 1, UserId = 1 }},
                new User { UserId = 2, AuthId = "auth2", Watchlist = new Watchlist { WatchlistId = 2, UserId = 2 }},
                new User { UserId = 3, AuthId = "auth3", Watchlist = new Watchlist { WatchlistId = 4, UserId = 3 }}
            };

            var mockWatchlist = new List<Watchlist>
            {
                new Watchlist { WatchlistId = 1, UserId = 1, Movies={new Movie{MovieId=1, Title="Action Movie"}, new Movie{MovieId=2, Title="Comic Movie"}}},
                new Watchlist { WatchlistId = 2, UserId = 2, Movies={}},
                new Watchlist { WatchlistId = 4, UserId = 3, Movies={new Movie{MovieId=2, Title="Comic Movie"}}},
            };

            Movie newMovie = new Movie{MovieId=3, Title="Animation"};

            // Repo : AddMovieToWatchlist
            _mockUserRepo.Setup(repo => repo.AddMovieToWatchlist(It.IsAny<string>(), It.IsAny<Movie>()))
                   .Callback((string authId, Movie movie) => mockUsers.FirstOrDefault(m => Equals(m.AuthId, authId))!.Watchlist!.Movies.Add(movie));
            
            // Act
            _ = await _userService.AddMovieToWatchlist("auth2", newMovie);
            var user = mockUsers.FirstOrDefault(u => u.AuthId.Equals("auth2"));
            // Assert
            Assert.Contains(user!.Watchlist!.Movies, m => m.Title.Equals(newMovie.Title));
            _mockUserRepo.Verify(repo => repo.AddMovieToWatchlist(It.IsAny<string>(), It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public async Task RemoveMovieFromWatchlist_ShouldRemoveMovie_FromWatchList()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, AuthId = "auth1", Watchlist = new Watchlist { WatchlistId = 1, UserId = 1 }},
                new User { UserId = 2, AuthId = "auth2", Watchlist = new Watchlist { WatchlistId = 2, UserId = 2 }},
                new User { UserId = 3, AuthId = "auth3", Watchlist = new Watchlist { WatchlistId = 4, UserId = 3 }}
            };

            var mockWatchlist = new List<Watchlist>
            {
                new Watchlist { WatchlistId = 1, UserId = 1, Movies={new Movie{MovieId=1, Title="Action Movie"}, new Movie{MovieId=2, Title="Comic Movie"}}},
                new Watchlist { WatchlistId = 2, UserId = 2, Movies={}},
                new Watchlist { WatchlistId = 4, UserId = 3, Movies={new Movie{MovieId=2, Title="Comic Movie"}}},
            };

            Movie targeMovie = new Movie{MovieId=2, Title="Comic Movie"};

            // Repo : RemoveMovieFromWatchlist
            _mockUserRepo.Setup(repo => repo.RemoveMovieFromWatchlist(It.IsAny<string>(), It.IsAny<Movie>()))
                   .Callback((string authId, Movie movie) => mockUsers.FirstOrDefault(m => Equals(m.AuthId, authId))!.Watchlist!
                   .Movies.RemoveAll(m=> m.MovieId == movie.MovieId));
            
            // Act
            _ = await _userService.RemoveMovieFromWatchlist("auth3", targeMovie);
            var user = mockUsers.FirstOrDefault(u => u.AuthId.Equals("auth3"));
            // Assert
            Assert.DoesNotContain(user!.Watchlist!.Movies, m => m.Title.Equals(targeMovie.Title));
            _mockUserRepo.Verify(repo => repo.RemoveMovieFromWatchlist(It.IsAny<string>(), It.IsAny<Movie>()), Times.Once);
        }         
    }
}