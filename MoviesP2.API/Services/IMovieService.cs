using MoviesP2.Models;

namespace MoviesP2.API.Services;

public interface IMovieService {
    public List<Movie> GetAllMovies();

    public Movie? GetMovieById(int id);

    public Movie? GetMovieByTitle(string title);

    public Movie AddMovie(Movie movie);

    public Movie EditMovie(Movie movie);

    public Movie? DeleteMovie(int movieId);
}