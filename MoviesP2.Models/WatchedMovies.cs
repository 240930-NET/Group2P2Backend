
using System.ComponentModel.DataAnnotations;

namespace MoviesP2.Models;

public class WatchedMovie
{
    public int MovieId {get; set;}
    public int UserId {get; set;}
    public Movie Movie {get; set;} = null!;
    public User User {get; set;} = null!;
    
}
