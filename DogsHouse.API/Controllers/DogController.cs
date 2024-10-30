using DogsHouse.API.Dtos;
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

    [HttpPost]
    public async Task<IActionResult> Add(DogAddDto? dto, CancellationToken cancellationToken)
    {
        if (dto != null)
        {
            await _dogService.AddAsync(dto, cancellationToken);
        
            return Ok("Dog was successfully added!");
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> List(string? attribute, string? order, CancellationToken cancellationToken)
    {
        var dogs = await _dogService.ListAsync(attribute, order, cancellationToken);
        
        return Ok(dogs);
    }
}