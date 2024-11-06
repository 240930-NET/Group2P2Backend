using Microsoft.EntityFrameworkCore;
using MoviesP2.Data.Repos;
using MoviesP2.Models;

namespace MoviesP2.Data;

public class WatchlistRepo : IWatchlistRepo{
    private readonly MoviesContext _context;

    public WatchlistRepo(MoviesContext context)
    {
        _context = context;
    }
    
    public List<Watchlist> GetAllWatchlists()
    {
        return _context.Watchlists.ToList();
    }
    public Watchlist? GetWatchlistById(int id)
    {
        return _context.Watchlists.Find(id);
    }
    
    public async Task<Watchlist?> GetWatchListByUserAuthId(string authId)
{
    var user = await _context.Users
        .Include(u => u.Watchlist)
        .SingleOrDefaultAsync(u => u.AuthId == authId);
    
    return user?.Watchlist;
}

    public void AddWatchlist(Watchlist watchlist)
    {
        _context.Watchlists.Add(watchlist);
        _context.SaveChanges();
    }
    public void DeleteWatchlist(Watchlist watchlist)
    {
        _context.Watchlists.Remove(watchlist);
        _context.SaveChanges();
    }
    //This will return an empty list if user has not watched any movies
    public List<Movie> GetWatchlistMovies(int id)
    {
        Watchlist ?found = GetWatchlistById(id);
        if (found != null)
        {
            return found.Movies;
        }
        else{
            throw new NullReferenceException();
        } 
    }
    //Not sure if these two below should just return the whole updated Watchlist or the updated movie
    //I put it as movie for now but subject to change
    public void AddMovieToWatchlist(int id, Movie movie) {
        Watchlist ?found = GetWatchlistById(id);
        if (found != null)
        {
            found.Movies.Add(movie);
            _context.SaveChanges();
        }
        else{
            throw new NullReferenceException();
        } 
    }
    public void RemoveMovieFromWatchlist(int id, Movie movie) {
            Watchlist ?found = GetWatchlistById(id);
            if (found != null)
            {
                found.Movies.Remove(movie);
                _context.SaveChanges();
            }
            else{
                throw new NullReferenceException();
            } 
        }

    public Watchlist? GetWatchlistByUserAuthId(string authId)
    {
        throw new NotImplementedException();
    }
}