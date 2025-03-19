using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookTaxi.Common2.Models.UserModel;
using BookTaxi.Services.Api;

namespace BookTaxi.Api.Controllers;


[Route("api/admin-action/accounts/admin-action/accounts/[controller]")]
[ApiController]
public class AdminAccountsController:ControllerBase
{
    private readonly AdminService _adminService;
    public AdminAccountsController(AdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateAdminModel model)
    {
        try
        {
            var result = await _adminService.AdminRegister(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("logIn")]
    public async Task<IActionResult> LogIn(CreateAdminModel model)
    {
        try
        {
            var token = await _adminService.AdminLogIn(model);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("account_id/profile")]
    [Authorize(Roles = "admin")]

    public async Task<IActionResult> AdminProfile(Guid Id)
    {
        try
        {
            var user = await _adminService.AdminProfile(Id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _adminService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}