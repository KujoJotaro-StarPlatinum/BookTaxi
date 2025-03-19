using BookTaxi.Common2.Models.CarModels;
using BookTaxi.Services.Api;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookTaxi.Common2.DTOs;
using BookTaxi.Common2.Models.CarModels;
using BookTaxi.Services.Api;

namespace BookTaxi.Api.Controllers;

[Route("api/drivers/driver_id/cars/car_id/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly CarService _carService;

    public CarController(CarService carService)
    {
        _carService = carService;
    }

    [HttpGet("all-cars")]
    public async Task<IActionResult> GetAllCars()
    {
        try
        {
            var cars = await _carService.GetAllCars();
            return Ok(cars);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("car-by-id/{id}")]
    public async Task<IActionResult> GetCarById(Guid id)
    {
        try
        {
            var car = await _carService.GetCarById(id);
            return Ok(car);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add-car")]
    public async Task<IActionResult> AddCar(AddCarModel model)
    {
        try
        {
            var car = await _carService.AddCar(model);
            return Ok(car);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}