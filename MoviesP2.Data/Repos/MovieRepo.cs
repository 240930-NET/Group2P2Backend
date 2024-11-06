using Microsoft.EntityFrameworkCore;
using MoviesP2.Data.Repos;
using MoviesP2.Models;

namespace MoviesP2.Data;

public class MovieRepo : IMovieRepo {
    private readonly MoviesContext _context;

    public MovieRepo(MoviesContext context)
    {
        _context = context;
    }
        //This probably never gets called by user but implement it for testing purposes
    public List<Movie> GetAllMovies()
    {
        return _context.Movies.ToList();
    }
    //Return the movie if its found or null if it isn't, handle null return in Service
    public Movie? GetMovieById(int id)
    {
        return _context.Movies.Find(id);
    }
    //Same as above
    public Movie? GetMovieByTitle(string title)
    {
        return _context.Movies.FirstOrDefault(m => Equals(m.Title, title));
    }
    //We may need some other functions to look for movies with different columns but do those as we go

    //Return the movie added/could also just be a void function may change this
    public void AddMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
    }
    //Edit an existing Movie / Return the edited Movie may need to return a nullable 
    //We either handle checking if movie in service/throw an error if it doesn't/make it nullable and return null if not exist
    public void EditMovie(Movie movie)
    {
        _context.Movies.Update(movie);
        _context.SaveChanges();
    }
    //Return Movie deleted or null if nothing deleted/Could return a boolean instead not sure
    public void DeleteMovie(Movie movie)
    {
        _context.Movies.Remove(movie);
        _context.SaveChanges();
    }
}