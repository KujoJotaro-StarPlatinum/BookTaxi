using BookTaxi.Common2.DTOs;
using BookTaxi.Services.Api;
using Microsoft.AspNetCore.Mvc;

namespace BookTaxi.Api.Controllers;

[Route("api/destination-action/[controller]")]
[ApiController]
public class DestinationController : ControllerBase
{
    private readonly DestinationService _destinationService;
    public DestinationController(DestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    [HttpPost("add-destination")]
    public async Task<IActionResult> AddDestination(DestinationDto model)
    {
        try
        {
            var destination = await _destinationService.AddDestination(model);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("all-destinations")]
    public async Task<IActionResult> GetAllDestinations()
    {
        try
        {
            var destinations = await _destinationService.GetAllDestination();
            return Ok(destinations);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
