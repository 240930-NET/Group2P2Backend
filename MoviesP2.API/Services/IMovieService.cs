using MoviesP2.Models;

namespace MoviesP2.API.Services;

public interface IMovieService {
    public List<Movie> GetAllMovies();
}