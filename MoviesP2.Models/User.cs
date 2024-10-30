using System.ComponentModel.DataAnnotations;

namespace MoviesP2.Models;

public class User
{
    [Key]
    public int UserId {get; set;}
    public string Username {get; set;} = null!;
    public Watchlist? Watchlist {get; set;}
    public List<WatchedMovie> WatchedMovies {get; } = [];
}