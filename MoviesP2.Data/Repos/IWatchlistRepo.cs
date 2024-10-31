using MoviesP2.Models;

namespace MoviesP2.Data.Repos;

public interface IWatchlistRepo {

    //Check IMovieRepo for general return type reasoning, got too lazy to retype comments
    //Some input variables may need to be changed to DTOs but I haven't looked into it yet
    //should not be too hard to change the logic if we end up using DTOs
    public List<Watchlist> GetAllWatchlists();
    public Watchlist? GetWatchlistById(int id);
    public Watchlist AddWatchlist(Watchlist watchlist); //probably should be a DTO here because movies will always be emtpy
    public Watchlist? DeleteWatchlist(int id);
    //This will return an empty list if user has not watched any movies
    public List<Movie> GetWatchlistMovies(int id);
    public Movie? AddMovieToWatchlist(int id, Movie movie);
}