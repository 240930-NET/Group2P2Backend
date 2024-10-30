using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesP2.Models;

public class Movie
{
    [Key]
    public int MovieId {get; set;}
    public string Title {get; set;} = null!;
    [Column(TypeName="Date")]
    public DateTime ReleaseDate { get; set; }
    public string? Rated {get; set;} //Rated R, PG-13, etc.

    public double Rating {get; set;} //8.10 on imbd
    public List<Watchlist> Watchlists {get; } = [];
    public List<User> Users {get; } = [];
}