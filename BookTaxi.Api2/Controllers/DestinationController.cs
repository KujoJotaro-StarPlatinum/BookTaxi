using BookTaxi.Common2.DTOs;
using BookTaxi.Services.Api;
using BookTaxiEntyties.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookTaxi.Api.Controllers;

[ApiController]
[Route("api/destination-action/[controller]")]
public class DestinationController : ControllerBase
{
    private readonly DestinationService _destinationService;
    private readonly IUserCarOrderRepository _userCarOrderRepository;

    public DestinationController(DestinationService destinationService, IUserCarOrderRepository userCarOrderRepository)
    {
        _destinationService = destinationService;
        _userCarOrderRepository = userCarOrderRepository;
    }

    [HttpPost("add-destination")]
    public async Task<IActionResult> AddDestination(DestinationDto model)
    {
        try
        {
            var destination = await _destinationService.AddDestination(model);
            return Ok(destination);
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
