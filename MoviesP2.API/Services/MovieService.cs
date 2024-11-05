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
}