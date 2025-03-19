using Microsoft.AspNetCore.Components.Forms;
using BookTaxi.Common2.DTOs;
using BookTaxi.Common2.Models.CarModels;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;
using BookTaxiEntyties.Repositiries;
using BookTaxi.Common2.Constants;

namespace BookTaxi.Services.Api;

public class CarService
{
    private readonly ICarRepository _carRepository;
    private readonly IUserCarRepository _userCarRepository;
    private readonly IUserCarRepository _userCar;
    private readonly IUserRepository _userRepository;
    public CarService(ICarRepository carRepository, IUserCarRepository userCarRepository, IUserCarRepository userCar, IUserRepository userRepository)
    {
        _carRepository = carRepository;
        _userCarRepository = userCarRepository;
        _userCar = userCar;
        _userRepository = userRepository;
    }

    public async Task<Cars> AddCar(AddCarModel model)
    {
        var user = await _userRepository.GetUserById(model.UserId);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }
        if (user.Role != Role.DriverRole)
        {
            throw new InvalidOperationException("You are not a driver");
        }
        Cars car = new()
        {
            Model = model.Model,
            CarName = model.CarName,
            Description = model.Description,
            UserId = model.UserId,
            Status = model.Status,
        };

        await _carRepository.AddCar(car);

        UserCars userCar = new()
        {
            IsOwner = true,
            UserId = model.UserId,
            CarId = car.Id,
        };

        _userCar.AddUserCar(userCar);
        return car;
    }

    private async Task<bool> CheckUserId(Guid Id)
    {
        var car = await _userRepository.GetUserById(Id);
        if (car is null)
        {
            return false;
        }
        return true;
    }

    public async Task<List<Cars>> GetAllCars()
    {
        var cars = await _carRepository.GetAllCars();
        return cars;
    }

    public async Task<Cars> GetCarById(Guid Id)
    {
        var car = await _carRepository.GetCarById(Id);
        return car;
    }
}