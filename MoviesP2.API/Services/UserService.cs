using MoviesP2.Data.Repos;
using MoviesP2.Models;

namespace MoviesP2.API.Services;

public class UserService : IUserService {

    private readonly IUserRepo _userRepo;
    public UserService (IUserRepo userRepo) {
        _userRepo = userRepo;
    }

    public async Task<List<User>> GetAllUsers() {
        return await _userRepo.GetAllUsers();
    }
    //AuthId will replace username, it is unique to each user that uses Auth0 to login. 
    //It might be possible for it to be null there is the need to check if it is null or not
    public async Task<User> GetUserByAuthId(string? authId) {
        if(authId != null) {
            User? user = await _userRepo.GetUserByAuthId(authId);
            if (user != null) return user;
            throw new Exception("User does not exist");
        }
        throw new Exception("No user info provided");
    }
    public async Task<Watchlist> GetUserWatchlist(string? authId) {
        if(authId != null) {
            Watchlist? watchlist = await _userRepo.GetUserWatchlist(authId);
            if (watchlist != null) return watchlist;
            throw new Exception("User does not exist");
        }
        throw new Exception("No user info provided");
    }
    public async Task<List<Movie>> GetUserWatchedMovies(string? authId) {
        if(authId != null) {
            List<Movie> watchedMovies = await _userRepo.GetUserWatchedMovies(authId);
            if (watchedMovies != null) return watchedMovies;
            throw new Exception("User does not exist");
        }
        throw new Exception("No user info provided");
    }
    //A new user is created with just a authId, so initialize a user with it and call the repo method with that user
    public async Task<User> AddUser(string? authId) {
        if (authId == null) throw new Exception("No user info provided");
        User? user = await _userRepo.GetUserByAuthId(authId);
        if (user != null) throw new Exception("User already exists");
        User userToAdd = new User {AuthId = authId, 
                                    Watchlist = new Watchlist()
                                };
        return await _userRepo.AddUser(userToAdd);
    }
    public async Task<User> DeleteUser(string? authId) {
        if (authId == null) throw new Exception("No user info provided");
        User? user = await _userRepo.GetUserByAuthId(authId) ?? throw new Exception("This user does not exist");
        await _userRepo.DeleteUser(user);
        return user;
    }
    //Currently returns a user may need to change I just wasn't sure
    //There is a different logic that needs to be used to check if the movie exists
    public async Task<User> AddWatchedMovie(string? authId, Movie movie) {
        if (authId == null) throw new Exception("No user info provided");
        User user = await _userRepo.GetUserByAuthId(authId) ?? throw new Exception("User doesn't exist");
        return await _userRepo.AddMovieToWatchedMovies(user.UserId, movie);
    }    
    public async Task<User> RemoveWatchedMovie(string? authId, Movie movie) {
        if (authId == null) throw new Exception("No user info provided");
        User user = await _userRepo.GetUserByAuthId(authId) ?? throw new Exception("User doesn't exist"); 
        return await _userRepo.RemoveMovieFromWatchedMovies(user.UserId, movie);
    }

    public async Task<User> AddMovieToWatchlist(string? authId, Movie movie) {
        if (authId == null) throw new Exception("No user info provided");
        User user = await _userRepo.GetUserByAuthId(authId) ?? throw new Exception("User doesn't exist");
        return await _userRepo.AddMovieToWatchlist(user.UserId, movie);
    }    
    public async Task<User> RemoveMovieFromWatchlist(string? authId, Movie movie) {
        if (authId == null) throw new Exception("No user info provided");
        User user = await _userRepo.GetUserByAuthId(authId) ?? throw new Exception("User doesn't exist"); 
        return await _userRepo.RemoveMovieFromWatchlist(user.UserId, movie);
    }
}