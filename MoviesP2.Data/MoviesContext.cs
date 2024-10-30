namespace MoviesP2.Data;
using Microsoft.EntityFrameworkCore;
using MoviesP2.Models;

public class MoviesContext : DbContext
{

    public DbSet<Movie> Movies {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<Watchlist> Watchlists {get; set;}
    public DbSet<WatchedMovie> WatchedMovies {get; set;}

    public MoviesContext() : base(){}
    public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) {}

    
}
