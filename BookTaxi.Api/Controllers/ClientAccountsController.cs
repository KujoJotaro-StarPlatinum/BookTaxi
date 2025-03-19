using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookTaxi.Common2.Models.UserModel;
using BookTaxi.Services.Api;

namespace BookTaxi.Api.Controllers;

[Route("api/client-action/[controller]")]

[ApiController]
public class ClientAccountsController: ControllerBase
{
    private readonly ClientService _clientService;
    public ClientAccountsController(ClientService userService)
    {
        _clientService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserModel model)
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

    [HttpPost("logIn")]
    public async Task<IActionResult> Login(LogInUserModel model)
    {
        try
        {
            var token = await _clientService.ClientLogIn(model);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("account_id/profile")]
    public async Task<IActionResult> ClientProfile(Guid Id)
    {
        try
        {
            var user = await _clientService.ClientProfile(Id);
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