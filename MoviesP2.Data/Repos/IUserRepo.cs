using MoviesP2.Models;

namespace MoviesP2.Data.Repos;

public interface IUserRepo{

    //Check IMovieRepo for general return type reasoning, got too lazy to retype comments
    //Some input variables may need to be changed to DTOs but I haven't looked into it yet
    //should not be too hard to change the logic if we end up using DTOs
    public List<User> GetAllUsers();
    public User? GetUserById(int id);
    public User AddUser(User user);
    public User? DeleteUser(int id);
    //I haven't completely gone over AuthO yet, still working through it so this may be needed
    //to enforce no duplicate usernames
    //Returns true if the username is not taken, false if it is
    public bool CheckNameNotTaken(User user);
    //Returns the user watchlist
    public Watchlist? GetUserWatchlist(int id);
    //This will return an empty list if user has not watched any movies
    public List<Movie> GetUserWatchedMovies(int id);
    public User? AddMovieToWatchedMovies(int id, Movie movie);
}