
using System.ComponentModel.DataAnnotations;

namespace MoviesP2.Models;

public class WatchedMovie
{
    [Key]
    public int WatchedMovieMovieId {get; set;}
    public List<User> Users {get; } = [];
    public int MovieId {get; set;}
    public Movie Movie {get; set;} = null!;
}
