namespace MoviesP2.Models;

public class Watchlist
{
    int WatchlistId {get; set;}
    int UserId {get; set;}
    User User {get; set;} = null!;
    List<Movie> Movies {get; } = [];
}
