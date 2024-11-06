using MoviesP2.Data.Repos;
using MoviesP2.Models;

namespace MoviesP2.API.Services;

public class MovieService : IMovieService {

    private readonly IMovieRepo _movieRepo;
    public MovieService (IMovieRepo movieRepo) {
        _movieRepo = movieRepo;
    }

    public void AddMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public void DeleteMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public void EditMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public List<Movie> GetAllMovies() {
        return _movieRepo.GetAllMovies();
    }

    public object? GetMovieById(int v)
    {
        throw new NotImplementedException();
    }
}