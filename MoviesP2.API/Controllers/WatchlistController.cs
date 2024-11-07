using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using MoviesP2.API.Services;
using MoviesP2.Models;

namespace MoviesP2.API.Controllers;


[ApiController] // this Data Annonation is marking our class as a controller
[EnableCors("TestingOnly")]
[Route("api/[controller]")]
public class WatchlistController : Controller
{
    
    private readonly IWatchlistService _watchlistService; // dependency injection 

    public WatchlistController(IWatchlistService watchlistService){
        _watchlistService = watchlistService;
    }

    [HttpGet] 
    [Authorize]
    public IActionResult GetAllWatchlists(){
        try{
            List<Watchlist> watchlists = _watchlistService.GetAllWatchlists();
            return Ok(watchlists);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }
}