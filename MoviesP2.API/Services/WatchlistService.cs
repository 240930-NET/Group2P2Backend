using MoviesP2.Data.Repos;
using MoviesP2.Models;

namespace MoviesP2.API.Services;

public class WatchlistService : IWatchlistService {

    private readonly IWatchlistRepo _watchlistRepo;
    public WatchlistService (IWatchlistRepo watchlistRepo) {
        _watchlistRepo = watchlistRepo;
    }

    public void AddMovieToWatchlist(string v, Movie movie)
    {
        throw new NotImplementedException();
    }

    public List<Watchlist> GetAllWatchlists() {
        return _watchlistRepo.GetAllWatchlists();
    }
    //May need to be changed 
    public Watchlist GetWatchlistByUserAuthId(string authId) {
        return _watchlistRepo.GetWatchlistByUserAuthId(authId)!;
    }

    public IEnumerable<object> GetWatchListByUserAuthId(string v)
    {
        throw new NotImplementedException();
    }

    public void RemoveMovieFromWatchlist(string v, Movie movie)
    {
        throw new NotImplementedException();
    }
}