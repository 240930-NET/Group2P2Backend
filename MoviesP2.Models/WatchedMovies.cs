
namespace MoviesP2.Models;

public class WatchedMovie
{
    int WatchedMovieMovieId {get; set;}
    List<User> Users {get; } = [];
    int MovieId {get; set;}
    Movie Movie {get; set;} = null!;
}
