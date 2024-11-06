using MoviesP2.Models;

namespace MoviesP2.Data.Repos;

public interface IUserRepo{

    //Check IMovieRepo for general return type reasoning, got too lazy to retype comments
    //Some input variables may need to be changed to DTOs but I haven't looked into it yet
    //should not be too hard to change the logic if we end up using DTOs
    public Task<List<User>> GetAllUsers();
    public Task<User?> GetUserById(int id);
    public Task<User?> GetUserByAuthId(string authId);
    public Task<User> AddUser(User user);
    public Task DeleteUser(User user);
    //I haven't completely gone over AuthO yet, still working through it so this may be needed
    //to enforce no duplicate usernames
    //Returns true if the username is not taken, false if it is
    public Task<bool> CheckAuthIdNotTaken(User user);
    //Returns the user watchlist
    public Task<Watchlist?> GetUserWatchlist(string authId);
    //This will return an empty list if user has not watched any movies
    public Task<List<Movie>> GetUserWatchedMovies(string authId);
    //Not sure if these two below should just return the whole updated User or the updated movie
    //I put it as movie for now but subject to change
    public Task<User> AddMovieToWatchedMovies(string authId, Movie movie);
    public Task<User> RemoveMovieFromWatchedMovies(string authId, Movie movie);
    public Task<User> AddMovieToWatchlist(string authId, Movie movie);
    public Task<User> RemoveMovieFromWatchlist(string authId, Movie movie);
    public Task<bool> CheckMovieInWatchedMovies(string authId, Movie movie);
    public Task<bool> CheckMovieInWatchlist(string authId, Movie movie);

}