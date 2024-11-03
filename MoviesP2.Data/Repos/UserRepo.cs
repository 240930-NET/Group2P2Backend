using Microsoft.EntityFrameworkCore;
using MoviesP2.Data.Repos;
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
    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
    public User? GetUserById(int id)
    {
        return _context.Users.Find(id);
    }
    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
    public bool CheckNameNotTaken(User user)
    {
        List<User> users = GetAllUsers();
        foreach(User curUser in users)
            if (curUser.Username.Equals(user.Username))
                return false;
        
        return true;
    }
    //Returns the user watchlist
    public Watchlist? GetUserWatchlist(int id)
    {
        User ?found = GetUserById(id);
        if (found != null)
        {
            return found.Watchlist;
        }
        else{
            throw new NullReferenceException();
        } 
    }
    //This will return an empty list if user has not watched any movies
    public List<Movie> GetUserWatchedMovies(int id)
    {
        User ?found = GetUserById(id);
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
    public void AddMovieToWatchedMovies(int id, Movie movie)
    {
        User ?found = GetUserById(id);
        if (found != null)
        {
            found.Movies.Add(movie);
            _context.SaveChanges();
        }
        else{
            throw new NullReferenceException();
        } 
    }
    public void RemoveMovieFromWatchedMovies(int id, Movie movie)
    {
        User ?found = GetUserById(id);
        if (found != null)
        {
            found.Movies.Remove(movie);
            _context.SaveChanges();
        }
        else{
            throw new NullReferenceException();
        } 
    }

}