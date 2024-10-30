using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DogController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Ping()
    {
        var message = "Dogshouseservice.Version1.0.1";
        
        return Ok(message);
    }
}