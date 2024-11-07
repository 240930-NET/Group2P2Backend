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

    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<Movie> GetMovieById(int id) {
        try {
            var movie = _movieService.GetMovieById(id);
            return Ok(movie);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("title/{title}")]
    [Authorize]
    public ActionResult<Movie> GetMovieByTtile(string title) {
        try {
            var movie = _movieService.GetMovieByTitle(title);
            return Ok(movie);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Movie> AddMovie([FromBody] Movie movie) {
        try {
            var newMovie = _movieService.AddMovie(movie);
            return Ok(newMovie);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    [Authorize]    
    public ActionResult<Movie> EditMovie([FromBody] Movie movie) {
        try {
            var editedMovie = _movieService.EditMovie(movie);
            return Ok(editedMovie);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }


    [HttpDelete("{id}")]
    [Authorize]    
    public ActionResult<Movie> DeleteMovie(int id) {
        try {
            var deletedMovie = _movieService.DeleteMovie(id);
            return Ok(deletedMovie);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }  
}