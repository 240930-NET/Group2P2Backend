using System.ComponentModel.DataAnnotations;

namespace MoviesP2.Models;

public class Watchlist
{
    [Key]
    public int WatchlistId {get; set;}
    public int UserId {get; set;}
    public User User {get; set;} = null!;
    public List<Movie> Movies {get; } = [];
}
