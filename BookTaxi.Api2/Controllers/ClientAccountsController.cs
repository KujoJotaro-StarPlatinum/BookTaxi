using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookTaxi.Common2.Models.UserModel;
using BookTaxi.Services.Api;

namespace BookTaxi.Api.Controllers;

[Route("api/client-action/[controller]")]

[ApiController]
public class ClientAccountsController : ControllerBase

{
    private readonly ClientService _clientService;
    private readonly DriverService _driverService;

    public ClientAccountsController(ClientService clientService, DriverService driverService)
    {
        _clientService = clientService;
        _driverService = driverService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateClientModel model)
    {
        try
        {
            var result = await _clientService.ClientRegister(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("verify-register")]
    public async Task<IActionResult> VerifyRegister(SmsModel model)
    {
        try
        {
            var result = await _clientService.VerifyRegister(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SmsLogInModel model)
    {
        try
        {
            var token = await _clientService.ClientLogin(model);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("verify-login")]
    public async Task<IActionResult> VerifyLogin(SmsModel model)
    {
        try
        {
            var result = await _clientService.VerifyLogin(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{account_id}/profile")]
    public async Task<IActionResult> ClientProfile(Guid id)
    {
        try
        {
            var user = await _clientService.ClientProfile(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("account_id/AllDrivers")]
    public async Task<IActionResult> ClientAllDrivers()
    {
        try
        {
            var drivers = await _clientService.GetListDriver();
            return Ok(drivers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}