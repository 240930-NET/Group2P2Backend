using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Auth0.AspNetCore.Authentication;

namespace MoviesP2.API.Controllers;

[ApiController] // this Data Annonation is marking our class as a controller
[Route("api/UserController")]
public class UserController : Controller
{
    /* BUNCH OF THINGS COMMENTED OUT TO TEST Auth0 
    
    private readonly IUserService _userService; // dependency injection 

    public UserController(IUserService userService){
        _userService = userService;
    }


    private readonly IMoviesRepo _movieRepo; // dependency injection 
    private readonly IUserRepo _userRepo;

    public UserController(IMoviesRepo movieRepo, IUserRepo _userRepo){
        _movieRepo = movieRepo;
    }
    */

    [HttpGet] 
    [Authorize]
    public IActionResult GetAllUsers(){
        try{
            return Ok("It worked");
           //return Ok(_userService.GetAllCustomers()); // return status ok and our List of Customers
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message); // return server error with the error message
        }
    }
}

