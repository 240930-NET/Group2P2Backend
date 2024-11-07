using Xunit;
using Moq;
using MoviesP2.API.Services;
using MoviesP2.Data.Repos;
using MoviesP2.Models;
using System.Collections.Generic;
using MoviesP2.Data;

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
        public void GetMovieByTitle_ShouldReturnMovie_WhenMovieExist()
        {
            // Arrange
            var mockMovies = new List<Movie>
            {
                new Movie { MovieId = 1, Title = "Comic Movie", ReleaseYear = 2020 },
                new Movie { MovieId = 2, Title = "Action Movie", ReleaseYear = 2021 }
            };
            _mockMovieRepo.Setup(repo => repo.GetMovieByTitle(It.IsAny<string>()))
                 .Returns((string title) => mockMovies.FirstOrDefault(i => Equals(i.Title, title)));

            Movie searchMovie = new Movie { MovieId = 1, Title = "Comic Movie", ReleaseYear = 2020 };                
            // Act
            var result = _movieService.GetMovieByTitle(searchMovie.Title);

            // Assert
            Assert.Equal(result.Title, searchMovie.Title);
            _mockMovieRepo.Verify(repo => repo.GetMovieByTitle(It.IsAny<string>()), Times.Once);
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
            var mockMovies = new List<Movie>
            {
                new Movie { MovieId = 1, Title = "Original Movie 1", ReleaseYear = 2020 , PosterLink = "8.8.8.8" },
                new Movie { MovieId = 2, Title = "Original Movie 2", ReleaseYear = 2021 , PosterLink = "8.8.8.8" }
            };
            var newMovie = new Movie { MovieId = 1, Title = "Original Movie 1", ReleaseYear = 2020, PosterLink = "9.9.9.9"};

            // Moc Repo : GetItemById
            _mockMovieRepo.Setup(repo => repo.GetMovieById(It.IsAny<int>()))
                  .Returns((int id) => mockMovies.FirstOrDefault(i => i.MovieId == id));

            // Moc Repo : CheckExistMovie
            _mockMovieRepo.Setup(repo => repo.CheckMovieExist(It.IsAny<Movie>()))
                  .Returns((Movie movie) => mockMovies
                  .Any(i => i.ReleaseYear == movie.ReleaseYear && Equals(i.Title, movie.Title)));

            // Act
            var result = _movieService.EditMovie(newMovie);

            // Assert
            Assert.Contains(mockMovies, i => i.PosterLink.Equals("9.9.9.9"));
            _mockMovieRepo.Verify(repo => repo.GetMovieById(newMovie.MovieId), Times.Once);
            _mockMovieRepo.Verify(repo => repo.EditMovie(It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public void DeleteMovie_ShouldCallRepoDeleteMovie()
        {
            // Arrange
            var mockMovies = new List<Movie>
            {
                new Movie { MovieId = 1, Title = "ActionMovie", ReleaseYear = 2020 , PosterLink = "8.8.8.8" },
                new Movie { MovieId = 2, Title = "OriginalMovie", ReleaseYear = 2021 , PosterLink = "8.8.8.8" }
            };
            Movie deleteMovie = new Movie { MovieId = 1, Title = "ActionMovie", ReleaseYear = 2020 , PosterLink = "8.8.8.8"};

            // Moc Repo : GetMovieById
            _mockMovieRepo.Setup(repo => repo.GetMovieById(It.IsAny<int>()))
                  .Returns((int id) => mockMovies.FirstOrDefault(i => i.MovieId == id));

            // Moc Repo : CheckExistMovie
            _mockMovieRepo.Setup(repo => repo.CheckMovieExist(It.IsAny<Movie>()))
                  .Returns((Movie movie) => mockMovies
                  .Any(i => i.ReleaseYear == movie.ReleaseYear && Equals(i.Title.ToLower(), movie.Title.ToLower())));


            // Moc Repo : Delete                    
            _mockMovieRepo.Setup(repo => repo.DeleteMovie(It.IsAny<Movie>()))
                .Callback<Movie>(movie => 
                {
                    var selectedMovie = mockMovies.Find(i => i.MovieId == movie.MovieId);
                    if (selectedMovie != null) {
                        mockMovies.Remove(selectedMovie);
                    }
                });

            // Act
            _movieService.DeleteMovie(deleteMovie);

            // Assert
            Assert.DoesNotContain(mockMovies, m => Equals(m.Title, deleteMovie.Title) && m.ReleaseYear == deleteMovie.ReleaseYear);
            _mockMovieRepo.Verify(repo => repo.GetMovieById(deleteMovie.MovieId), Times.Once);
        }
    }
}