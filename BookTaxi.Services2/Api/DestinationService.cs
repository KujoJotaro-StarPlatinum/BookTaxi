using BookTaxi.Common2.DTOs;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxi.Services.Api;

public class DestinationService
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly ICarRepository _carRepository;
    public DestinationService(IDestinationRepository destinationRepository, ICarRepository carRepository)
    {
        _destinationRepository = destinationRepository;
        _carRepository = carRepository;
    }

    public async Task<List<Destination>> GetAllDestination()
    {
        var destination = await _destinationRepository.GetAllDestination();
        if (destination == null)
        {
            throw new Exception("Destinations not found");
        }
        return destination;
    }

    public async Task<Destination> AddDestination(DestinationDto model)
    {
        var car = await _carRepository.GetCarById(model.CarId);
        if (car is null)
        {
            throw new Exception("Car not found");
        }
        Destination destination = new()
        {
            Id = Guid.NewGuid(),
            FromWhere = model.FromWhere,
            ToWhere = model.ToWhere,
            CarId = model.CarId,
            Car = car,
        };
        car.Destination = destination;
        car.DestinationId = destination.Id;
        await _destinationRepository.AddDestination(destination);
        await _carRepository.Update(car);
        return destination;
    }
}