using System.ComponentModel.DataAnnotations;
using DogsHouse.API.Dtos;
using DogsHouse.API.QueryParameters;
using DogsHouse.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DogsHouse.API.Controllers;

[ApiController]
[Route("[controller]s")]
public class DogController : ControllerBase
{
    private readonly IDogService _dogService;

    public DogController(IDogService dogService)
    {
        _dogService = dogService;
    }
    
    [EnableRateLimiting("fixed")]
    [HttpPost]
    public async Task<IActionResult> Add([Required]DogAddDto dto, CancellationToken cancellationToken)
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
    public async Task<IActionResult> List([FromQuery]DogQueryParameter? parameters, CancellationToken cancellationToken)
    {
        try
        {
            var dogs = await _dogService.ListAsync(parameters, cancellationToken);

            return Ok(dogs);
        }
        catch (Exception)
        {
            return StatusCode(500, "Some troubles on server side");
        }
    }
}
