using Microsoft.EntityFrameworkCore;
using MoviesP2.Models;

namespace MoviesP2.Data.Repos;

public class UserRepo : IUserRepo{

    private readonly MoviesContext _context;

    public UserRepo(MoviesContext context)
    {
        _context = context;
    }

    //Check IMovieRepo for general return type reasoning, got too lazy to retype comments
    //Some input variables may need to be changed to DTOs but I haven't looked into it yet
    //should not be too hard to change the logic if we end up using DTOs
    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users
                    .Include(u => u.Watchlist)
                    .Include(u => u.Movies)
                    .ToListAsync();
    }
    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users
                    .Include(u => u.Watchlist)
                    .Include(u => u.Movies)
                    .SingleOrDefaultAsync(u => u.UserId == id);
    }
    public async Task<User?> GetUserByAuthId(string authId) {
        return await _context.Users
                    .Include(u => u.Watchlist)
                    .Include(u => u.Movies)
                    .SingleOrDefaultAsync(u => u.AuthId == authId);
    }
    public async Task<User> AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task DeleteUser(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> CheckAuthIdNotTaken(User user) //May need to be removed for duplicate use
    {
        User? foundUser = await _context.Users
                                .SingleOrDefaultAsync(u => u.AuthId == user.AuthId);
        if (foundUser == null) return true;
        return false;
    }
    //Returns the user watchlist
    public async Task<Watchlist?> GetUserWatchlist(string authId)
    {
        User ?found = await GetUserByAuthId(authId);
        if (found != null)
        {
            return found.Watchlist;
        }
        else{
            throw new NullReferenceException();
        } 
    }
    //This will return an empty list if user has not watched any movies
    public async Task<List<Movie>> GetUserWatchedMovies(string authId)
    {
        User ?found = await GetUserByAuthId(authId);
        if (found != null)
        {
            return found.Movies;
        }
        else{
            throw new NullReferenceException();
        } 
    }
    //Not sure if these two below should just return the whole updated User or the updated movie
    //I put it as movie for now but subject to change
    public async Task<User> AddMovieToWatchedMovies(int id, Movie movie)
    {
        User ?found = await GetUserById(id);
        if (found != null)
        {   
            Movie? foundMovie = await _context.Movies
                .SingleOrDefaultAsync(m => m.Title == movie.Title 
                                        && m.ReleaseYear == movie.ReleaseYear);
            if (foundMovie == null) {
                found.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }
            else {
                if(found.Movies.Contains(foundMovie)) throw new Exception("This movie is already in the user's watchlist");
                found.Movies.Add(foundMovie);
                await _context.SaveChangesAsync();
            }
            return found;
        }
        else{
            throw new NullReferenceException();
        } 
    }
    public async Task<User> RemoveMovieFromWatchedMovies(int id, Movie movie)
    {
        User ?found = await GetUserById(id);
        if (found != null)
        {
            found.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return found;
        }
        else{
            throw new NullReferenceException();
        } 
    }

    public async Task<User> AddMovieToWatchlist(int id, Movie movie)
    {
        User ?found = await GetUserById(id);
        if (found != null)
        {   
            if (found.Watchlist == null) throw new Exception("User watchlist is null");
            Movie? foundMovie = await _context.Movies
                .SingleOrDefaultAsync(m => m.Title == movie.Title 
                                        && m.ReleaseYear == movie.ReleaseYear);
            if (foundMovie == null) {
                found.Watchlist.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }
            else {
                if(found.Watchlist.Movies.Contains(foundMovie)) throw new Exception("This movie is already in the user's watchlist");
                found.Watchlist.Movies.Add(foundMovie);
                await _context.SaveChangesAsync();
            }
            return found;
        }
        else{
            throw new NullReferenceException();
        } 
    }
    public async Task<User> RemoveMovieFromWatchlist(int id, Movie movie)
    {
        User ?found = await GetUserById(id);
        if (found != null)
        {
            if (found.Watchlist == null) throw new Exception("User watchlist is null");
            found.Watchlist.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return found;
        }
        else{
            throw new NullReferenceException();
        } 
    }
}