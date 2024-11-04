using DogsHouse.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DogsHouse.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    private readonly IPingService _pingService;

    public PingController(IPingService pingService)
    {
        _pingService = pingService;
    }

    [EnableRateLimiting("fixed")]
    [HttpGet]
    public IActionResult Ping()
    {
        var message = _pingService.Ping();

        return Ok(message);
    }
}