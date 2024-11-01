using MoviesP2.Models;

namespace MoviesP2.Repositories;
public interface IMovieRepo {
    public List<Movie> GetAllMovies();
    //public List<Movie> GetAllMovies();
    public Watchlist GetUserWatchlist(int id);
    public Movie GetMovieByTitle(string title);
}