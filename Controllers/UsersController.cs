using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi; 
using aeroWebApi.Services;
using aeroWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace aeroWebApi.Controllers;

// GET /api/passengers
// POST /api/passengers
// GET /api/passengers/{id}
// DELETE /api/passengers/{id}

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;
    public UsersController(UserService service)
    {
        _service = service;
    }

    // GET /api/users
    [HttpGet]
     
    // GET /api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _service.GetUserById(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }


    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto userDto)
    {
        try
        {
            var createdUser = await _service.CreateUser(userDto);
//ASP.NET Core MVC/Web API helper that returns a 201 Created response with a Location header pointing to where the newly created resource can be retrieved.

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = createdUser.Id },
                createdUser);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}




