using MoviesP2.Data.Repos;
using MoviesP2.Models;

namespace MoviesP2.API.Services;

public class MovieService : IMovieService {

    private readonly IMovieRepo _movieRepo;
    public MovieService (IMovieRepo movieRepo) {
        _movieRepo = movieRepo;
    }

    public List<Movie> GetAllMovies() {
        return _movieRepo.GetAllMovies();
    }

     public Movie? GetMovieById(int id) {
        var movie = _movieRepo.GetMovieById(id);

        if (movie == null) {
            return null;
        }
        return movie;
    }

    public Movie? GetMovieByTitle(string title) {
        var movie = _movieRepo.GetMovieByTitle(title);

        if (movie == null) {
            return null;
        }

        return movie;
    }

    public Movie AddMovie(Movie movie) {
        if (string.IsNullOrWhiteSpace(movie.Title))
        {
            throw new ArgumentException("Movie title cannot be empty.");
        }

        try {
            _movieRepo.AddMovie(movie);
            return movie;
        } catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public Movie EditMovie(Movie movie)  {
        if (string.IsNullOrWhiteSpace(movie.Title))
        {
            throw new ArgumentException("Movie title cannot be empty.");
        }
        var existingMovie = _movieRepo.GetMovieById(movie.MovieId);
        
        if (existingMovie == null) {
            throw new Exception("Movie not found");
        }
        existingMovie.Title = movie.Title;
        existingMovie.ReleaseYear = movie.ReleaseYear;
        existingMovie.PosterLink = movie.PosterLink;

        _movieRepo.EditMovie(existingMovie);

        return existingMovie;
    }
    
    public Movie? DeleteMovie(int movieId) {
        var existingMovie = _movieRepo.GetMovieById(movieId);
        
        if (existingMovie == null) {
            throw new Exception("Movie not found");
        }
        _movieRepo.DeleteMovie(existingMovie);
        
        return existingMovie;  
    }
}