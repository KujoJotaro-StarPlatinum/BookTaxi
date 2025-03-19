using BookTaxi.Common2.Models.UserModel;
using BookTaxi.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTaxi.Api.Controllers;

[ApiController]
[Route("driver-action/accounts/[controller]")]
public class DriverAccountsController : ControllerBase
{
    private readonly DriverService _driverService;
    private readonly CarService _carService;

    public DriverAccountsController(DriverService driverService, CarService carService)
    {
        _driverService = driverService;
        _carService = carService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> DriverRegister(CreateDriverModel model)
    {
        try
        {
            var user = await _driverService.DriverRegister(model);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> DriverLogin(LogInDriverModel model)
    {
        try
        {
            var user = await _driverService.DriverLogin(model);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "admin,driver")]
    [HttpGet("{account_id}/profile")]
    public async Task<IActionResult> DriverProfile(Guid id)
    {
        try
        {
            var user = await _driverService.DriverProfile(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
