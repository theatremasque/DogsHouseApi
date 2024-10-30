using DogsHouse.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DogController : ControllerBase
{
    private readonly IDogService _dogService;

    public DogController(IDogService dogService)
    {
        _dogService = dogService;
    }

    [HttpGet]
    public async Task<IActionResult> Ping()
    {
        var message = _dogService.Ping();
        
        return Ok(message);
    }
}