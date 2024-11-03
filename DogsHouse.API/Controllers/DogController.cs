using DogsHouse.API.Dtos;
using DogsHouse.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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
    
    [EnableRateLimiting("fixed")]
    [HttpGet]
    public async Task<IActionResult> Ping()
    {
        var message = _dogService.Ping();
        
        return Ok(message);
    }
    
    [EnableRateLimiting("fixed")]
    [HttpPost]
    public async Task<IActionResult> Add(DogAddDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var dog = await _dogService.AddAsync(dto, cancellationToken);

            return Ok(dog);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [EnableRateLimiting("fixed")]
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] string? attribute, [FromQuery] string? order, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, CancellationToken cancellationToken)
    {
        var dogs = await _dogService.ListAsync(attribute, order, pageNumber, pageSize, cancellationToken);
        
        return Ok(dogs);
    }
}