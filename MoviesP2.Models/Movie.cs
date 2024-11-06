using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesP2.Models;

public class Movie
{
    [Key]
    public int MovieId {get; set;}
    public string Title {get; set;} = null!;
    public int ReleaseYear { get; set; }
    public string? PosterLink {get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public List<Watchlist> Watchlists {get; } = [];
    [System.Text.Json.Serialization.JsonIgnore]
    public List<User> Users {get; } = [];
}