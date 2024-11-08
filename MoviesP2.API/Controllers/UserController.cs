using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using MoviesP2.API.Services;
using MoviesP2.Models;
using System.Data;

namespace MoviesP2.API.Controllers;

[ApiController] // this Data Annonation is marking our class as a controller
[Route("api/[controller]")]
public class UserController : Controller
{
    
    private readonly IUserService _userService; // dependency injection 

    public UserController(IUserService userService){
        _userService = userService;
    }

    //Used for testing to be removed in prod
    [HttpGet] 
    [Authorize]
    public async Task<IActionResult> GetAllUsers(){
        try{
            List<User> users = await _userService.GetAllUsers();
            return Ok(users);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpGet("userInfo")]
    [Authorize]
    public async Task<IActionResult> GetUserByAuthId() {
        try {
            User user = await _userService.GetUserByAuthId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(user);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpGet("userWatchlist")]
    [Authorize]
    public async Task<IActionResult> GetUserWatchlist() {
        try {
            List<Movie> watchlistMovies = await _userService.GetUserWatchlist(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(watchlistMovies);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpGet("userWatchedMovies")]
    [Authorize]
    public async Task<IActionResult> GetUserWatchedMovies() {
        try {
            List<Movie> watchedMovies = await _userService.GetUserWatchedMovies(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(watchedMovies);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpPost("addUser")]
    [Authorize]
    public async Task<IActionResult> AddUser([FromBody] User user) {
        try {
            User createdUser = await _userService.AddUser(user.AuthId);
            return Ok(createdUser);
        }
        catch(Exception ex){
            if(ex is DuplicateNameException) {
                return Ok("Duplicate user");
            }
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpDelete("deleteUser")]
    [Authorize]
    public async Task<IActionResult> DeleteUser() {
        try {
            User user = await _userService.DeleteUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(user);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpPatch("userAddWatchedMovie")]
    [Authorize]
    public async Task<IActionResult> AddWatchedMovie([FromBody] Movie movie) {
        try {
            User user = await _userService.AddWatchedMovie(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, movie);
            return Ok(user);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpPatch("userRemoveWatchedMovie")]
    [Authorize]
    public async Task<IActionResult> RemoveWatchedMovie([FromBody] Movie movie) {
        try {
            User user = await _userService.RemoveWatchedMovie(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, movie);
            return Ok(user);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpPatch("userAddMovieToWatchlist")]
    [Authorize]
    public async Task<IActionResult> AddMovieToWatchlist([FromBody] Movie movie) {
        try {
            User user = await _userService.AddMovieToWatchlist(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, movie);
            return Ok(user);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }

    [HttpPatch("userRemoveWatchlistMovie")]
    [Authorize]
    public async Task<IActionResult> RemoveMovieFromWatchlist([FromBody] Movie movie) {
        try {
            User user = await _userService.RemoveMovieFromWatchlist(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, movie);
            return Ok(user);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }
    
    [HttpGet("checkMovieInWatchedMovie/{title}/{releaseYear}/{posterLink}")]
    [Authorize]
    public async Task<IActionResult> CheckMovieInWatchedMovie(string title, int releaseYear, string posterLink) {
        Movie movie = new() { Title = title, ReleaseYear = releaseYear, PosterLink=posterLink};
        try {
            bool result = await _userService.CheckMovieInWatchedMovies(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, movie);
            return Ok(result);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }
    
    [HttpGet("checkMovieInWatchlist/{title}/{releaseYear}/{posterLink}")]
    [Authorize]
    public async Task<IActionResult> CheckMovieInWatchlist(string title, int releaseYear, string posterLink) {
        Movie movie = new() { Title = title, ReleaseYear = releaseYear, PosterLink=posterLink};
        try {
            User user = await _userService.RemoveMovieFromWatchlist(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, movie);
            return Ok(user);
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }
}

