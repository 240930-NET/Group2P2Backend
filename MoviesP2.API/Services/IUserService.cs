using MoviesP2.Models;

namespace MoviesP2.API.Services;

public interface IUserService {

    public Task<List<User>> GetAllUsers();
    //AuthId will replace username, it is unique to each user that uses Auth0 to login. 
    //It might be possible for it to be null there is the need to check if it is null or not
    public Task<User> GetUserByAuthId(string? authId);
    public Task<Watchlist> GetUserWatchlist(string? authId);
    public Task<List<Movie>> GetUserWatchedMovies(string? authId);
    //A new user is created with just a authId, so initialize a user with it and call the repo method with that user
    public Task<User> AddUser(string? authId);
    public Task<User> DeleteUser(string? authId);
    //Currently returns a user may need to change I just wasn't sure
    public Task<User> AddWatchedMovie(string? authId, Movie movie);
    public Task<User> RemoveWatchedMovie(string? authId, Movie movie);

}