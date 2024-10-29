namespace MoviesP2.Models;

public class User
{
    int UserId {get; set;}
    string Username {get; set;} = null!;
    Watchlist? Watchlist {get; set;}
    List<WatchedMovie> WatchedMovies {get; } = [];
}