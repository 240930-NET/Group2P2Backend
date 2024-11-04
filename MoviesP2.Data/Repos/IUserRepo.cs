using MoviesP2.Models;

namespace MoviesP2.Data.Repos;

public interface IUserRepo{

    //Check IMovieRepo for general return type reasoning, got too lazy to retype comments
    //Some input variables may need to be changed to DTOs but I haven't looked into it yet
    //should not be too hard to change the logic if we end up using DTOs
    public List<User> GetAllUsers();
    public User? GetUserById(int id);
    public void AddUser(User user);
    public void DeleteUser(User user);
    //I haven't completely gone over AuthO yet, still working through it so this may be needed
    //to enforce no duplicate usernames
    //Returns true if the username is not taken, false if it is
    public bool CheckNameNotTaken(User user);
    //Returns the user watchlist
    public Watchlist? GetUserWatchlist(int id);
    //This will return an empty list if user has not watched any movies
    public List<Movie> GetUserWatchedMovies(int id);
    //Not sure if these two below should just return the whole updated User or the updated movie
    //I put it as movie for now but subject to change
    public void AddMovieToWatchedMovies(int id, Movie movie);
    public void RemoveMovieFromWatchedMovies(int id, Movie movie);

}