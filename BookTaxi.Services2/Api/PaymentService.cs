using BookTaxi.Common2.Constants;
using BookTaxi.Common2.Models.PaymentModels;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;
using System.Security.Cryptography.X509Certificates;
using BookTaxi.Common2.DTOs;

namespace BookTaxi.Services.Api;

public class PaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICarRepository _carRepository;
    public PaymentService(IPaymentRepository paymentRepository, IUserRepository userRepository, ICarRepository carRepository)
    {
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _carRepository = carRepository;
    }

    public async Task<List<Payments>> GetAllPayments()
    {
        var payments = await _paymentRepository.GetAllPayments();
        if (payments == null)
        {
            throw new Exception("Payment not null");
        }
        return payments;
    }

    public async Task<Payments> GetByIdPayment(int paymentId)
    {
        var payment = await _paymentRepository.GetByIdPayment(paymentId);
        if (payment is null)
        {
            throw new Exception("This id not found");
        }
        return payment;
    }

    public async Task<Payments> AddPayment(PaymentAdd model)
    {
        try
        {
            var userId = await _userRepository.GetUserById(model.UserId);
            if (userId == null && userId.Role != Role.DriverRole)
            {
                throw new Exception("User in valid");
            }

            var carId = await _carRepository.GetCarById(model.CarId);
            if (carId == null)
            {
                throw new Exception("Car in valid");
            }

            Payments paymets = new Payments()
            {
                Amount = model.Amount,
                PaymentDate = DateTime.UtcNow,
                Status = model.Status,
                PaymentMethod = model.PaymentMethod,
            };
            await _paymentRepository.AddPayment(paymets);
            return paymets;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}