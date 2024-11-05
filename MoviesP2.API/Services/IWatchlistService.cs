using MoviesP2.Models;

namespace MoviesP2.API.Services;

public interface IWatchlistService {
    //Lowkey don't know how many of these we actually need
    //Maybe just delete and add
    //Most of the lookup for users should be by Auth0id
    //Maybe we should just make that the primary key of the user table
    //So that it is easier to reference the user from 
    //the watchlist repo on creation
    public List<Watchlist> GetAllWatchlists();
    //May need to be changed 
    public Watchlist GetWatchlistByUserAuthId(string authId); 

}
