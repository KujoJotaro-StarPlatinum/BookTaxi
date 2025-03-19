using BookTaxi.Common2.Models.UserModel;
using BookTaxi.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookTaxi.Common2.Models.UserModel;

namespace BookTaxi.Api.Controllers;

[Route("driver-action/accounts/[controller]")]
[ApiController()]

public class DriverAccountsController:ControllerBase
{
    private readonly DriverService _driverService;
    public DriverAccountsController(DriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> DriverRegister(CreateDriverModel model)
    {
        try
        {
            var user = _driverService.DriverRegister(model);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("logIn")]
    public async Task<IActionResult> DriverLogIn(LogInDriverModel model)
    {
        try
        {
            var user = _driverService.DriverLogIn(model);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "admin,driver")]
    [HttpGet("account_id/profile")]
    public async Task<IActionResult> DriverProfile(Guid Id)
    {
        try
        {
            var user = await _driverService.DriverProfile(Id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}