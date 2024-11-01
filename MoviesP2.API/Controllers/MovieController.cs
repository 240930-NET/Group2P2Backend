using MoviesP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MoviesP2.API;

[ApiController]
[Route("api/[controller]")]
public class MovieController : Controller{
    /*
    private readonly IMovieService _roomService;

    public RoomController(IRoomService roomService){
        _roomService = roomService;
    }

    [HttpGet]
    public IActionResult GetRooms(){
        try{
            return Ok(_roomService.GetAllRooms());
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("getRoomByName/{name}")]
    public IActionResult GetRoomByName(string name)
    {
        try{
            Room ?searchedRoom = _roomService.GetRoomByName(name);
            return Ok(searchedRoom);
        }
        catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("addNewRoom")]
    public IActionResult AddNewRoom([FromBody]Room room){
        try{
            _roomService.AddRoom(room);
            return Ok(room);
        }
        catch(Exception){
            return BadRequest("Could not add room");
        }
    }

    [HttpPut("editRoom")]
    public IActionResult EditRoom([FromBody] Room room){
        try{
            _roomService.EditRoom(room);
            return Ok(room);
        }
        catch (Exception)
        {
            return BadRequest("Could not edit room");
        }
    }

    [HttpDelete("deleteRoom/{name}")]
    public IActionResult DeleteRoom(string name){
        try{
            _roomService.DeleteRoom(name);
            return Ok("Room deleted");
        }
        catch(Exception){
            return BadRequest("Could not delete room");
        }
    }
    */
}