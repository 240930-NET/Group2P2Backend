using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesP2.Models;

public class Movie
{
    int MovieId {get; set;}
    string Title {get; set;} = null!;
    [Column(TypeName="Date")]
    public DateTime ReleaseDate { get; set; }
    string? Rated {get; set;} //Rated R, PG-13, etc.

    double Rating {get; set;} //8.10 on imbd
    List<Watchlist> Watchlists {get; } = [];
    WatchedMovie? WatchedMovie {get; set;}
}