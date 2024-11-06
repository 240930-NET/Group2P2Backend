using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

using MoviesP2.Models;
using MoviesP2.API.Services;

namespace MoviesP2.API.Controllers;

[ApiController]
[EnableCors("TestingOnly")]
[Route("api/[controller]")]
public class MovieController : Controller{
    
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService){
        _movieService = movieService;
    }

    [HttpGet] 
    [Authorize]
    public IActionResult GetAllMovies(){
        try{
            List<Movie> movies = _movieService.GetAllMovies();
            return Ok(movies);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }
}