using MoviesP2.Models;

namespace MoviesP2.Data.Repos;

//Provide all CRUD operations you need to
public interface IMovieRepo{

    //This probably never gets called by user but implement it for testing purposes
    public List<Movie> GetAllMovies(); 
    //Return the movie if its found or null if it isn't, handle null return in Service
    public Movie? GetMovieById(int id);
    //Same as above
    public Movie? GetMovieByTitle(string title);
    //We may need some other functions to look for movies with different columns but do those as we go

    //Return the movie added/could also just be a void function may change this
    public void AddMovie(Movie movie);
    //Edit an existing Movie / Return the edited Movie may need to return a nullable 
    //We either handle checking if movie in service/throw an error if it doesn't/make it nullable and return null if not exist
    public void EditMovie(Movie movie);
    //Return Movie deleted or null if nothing deleted/Could return a boolean instead not sure
    public void DeleteCustomer(Movie movie);

}