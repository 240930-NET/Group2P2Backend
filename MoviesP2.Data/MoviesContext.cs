namespace MoviesP2.Data;
using Microsoft.EntityFrameworkCore;

public class MoviesContext
{
    public MoviesContext() : base(){}
    //public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) {}

    public DbSet<MoviesContext> Movies {get; set;}
}
