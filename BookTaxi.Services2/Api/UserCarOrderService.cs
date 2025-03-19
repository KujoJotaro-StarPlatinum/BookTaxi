using BookTaxi.Common2.DTOs;
using BookTaxi.Services.Api;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

public class UserCarOrderService
{
    private readonly IUserCarOrderRepository _userCarOrderRepository;
    private readonly IUserRepository _userRepository;
    private readonly CarService _carService;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IDestinationRepository _destinationRepository;
    public UserCarOrderService(IUserCarOrderRepository userCarOrderRepository, CarService carService, IUserRepository userRepository, IPaymentRepository paymentRepository, IDestinationRepository destinationRepository)
    {
        _userCarOrderRepository = userCarOrderRepository;
        _carService = carService;
        _userRepository = userRepository;
        _paymentRepository = paymentRepository;
        _destinationRepository = destinationRepository;
    }

    public async Task<List<UserCarsOrders>> GetAllOrdes()
    {
        var orders = await _userCarOrderRepository.GetAllOrders();
        return orders;
    }

    public async Task<UserCarsOrders> AddOrder(UserCarsOrdersDto dtos)
    {
        var car = await _carService.GetCarById(dtos.CarId);
        if (car is null)
        {
            throw new Exception("Car is null");
        }

        var user = await _userRepository.GetUserById(dtos.UserId);
        if (user is null)
        {
            throw new Exception("User is not found");
        }

        UserCarsOrders userCarOrder = new()
        {
            CarId = dtos.CarId,
            UserId = dtos.UserId,
            ToLocation = car.Destination.ToWhere,
            FromLocation = car.Destination.FromWhere,
            Price = dtos.Price,
            Place = dtos.Place,
            Description = dtos.Description,
        };
        _userCarOrderRepository.AddUserCarOrder(userCarOrder);
        return userCarOrder;
    }
}