using System.ComponentModel.DataAnnotations;

namespace MoviesP2.Models;

public class User
{
    [Key]
    public int UserId {get; set;}
    public string AuthId {get; set;} = null!;
    public Watchlist? Watchlist {get; set;}
    public List<Movie> Movies {get; } = [];
}