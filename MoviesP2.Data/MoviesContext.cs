namespace MoviesP2.Data;
using Microsoft.EntityFrameworkCore;
using MoviesP2.Models;

public class MoviesContext : DbContext
{

    public DbSet<Movie> Movies {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<Watchlist> Watchlists {get; set;}
    //public DbSet<WatchedMovie> WatchedMovies {get; set;}

    public MoviesContext() : base(){}
    public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasMany(e => e.Users)
            .WithMany(e => e.Movies)
            .UsingEntity(
                "WatchedMovie",
                l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UserId").HasPrincipalKey(nameof(User.UserId)),
                r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("MovieId").HasPrincipalKey(nameof(Movie.MovieId)),
                j => j.HasKey("MovieId", "UserId"));
    }  
}
