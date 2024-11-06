using Xunit;
using Moq;
using MoviesP2.API.Services;
using MoviesP2.Data.Repos;
using MoviesP2.Models;
using System.Collections.Generic;

namespace MoviesP2.Tests.Services
{
    public class MovieServiceTests
    {
        private readonly MovieService _movieService;
        private readonly Mock<IMovieRepo> _mockMovieRepo;

        public MovieServiceTests()
        {
            _mockMovieRepo = new Mock<IMovieRepo>();
            _movieService = new MovieService(_mockMovieRepo.Object);
        }

        [Fact]
        public void GetAllMovies_ShouldReturnListOfMovies()
        {
            // Arrange
            var mockMovies = new List<Movie>
            {
                new Movie { MovieId = 1, Title = "Movie 1", ReleaseYear = 2020 },
                new Movie { MovieId = 2, Title = "Movie 2", ReleaseYear = 2021 }
            };
            _mockMovieRepo.Setup(repo => repo.GetAllMovies()).Returns(mockMovies);

            // Act
            var result = _movieService.GetAllMovies();

            // Assert
            Assert.Equal(2, result.Count);
            _mockMovieRepo.Verify(repo => repo.GetAllMovies(), Times.Once);
        }

        [Fact]
        public void GetMovieById_ShouldReturnMovie_WhenMovieExists()
        {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Movie 1" };
            _mockMovieRepo.Setup(repo => repo.GetMovieById(1)).Returns(movie);

            // Act
            var result = _movieService.GetMovieById(1);

            // Assert
            Assert.Equal(movie, result);
            _mockMovieRepo.Verify(repo => repo.GetMovieById(1), Times.Once);
        }

        [Fact]
        public void GetMovieById_ShouldReturnNull_WhenMovieDoesNotExist()
        {
            // Arrange
            _mockMovieRepo.Setup(repo => repo.GetMovieById(1)).Returns((Movie)null);

            // Act
            var result = _movieService.GetMovieById(1);

            // Assert
            Assert.Null(result);
            _mockMovieRepo.Verify(repo => repo.GetMovieById(1), Times.Once);
        }

        [Fact]
        public void AddMovie_ShouldCallRepoAddMovie()
        {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Movie 1" };

            // Act
            _movieService.AddMovie(movie);

            // Assert
            _mockMovieRepo.Verify(repo => repo.AddMovie(movie), Times.Once);
        }

        [Fact]
        public void EditMovie_ShouldCallRepoEditMovie()
        {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Updated Movie" };

            // Act
            _movieService.EditMovie(movie);

            // Assert
            _mockMovieRepo.Verify(repo => repo.EditMovie(movie), Times.Once);
        }

        [Fact]
        public void DeleteMovie_ShouldCallRepoDeleteMovie()
        {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Movie to Delete" };

            // Act
            _movieService.DeleteMovie(movie);

            // Assert
            _mockMovieRepo.Verify(repo => repo.DeleteCustomer(movie), Times.Once);
        }
    }
}