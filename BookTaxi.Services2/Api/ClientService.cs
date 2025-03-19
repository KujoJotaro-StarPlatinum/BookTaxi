using Microsoft.AspNetCore.Identity;
using BookTaxi.Common2.Models.UserModel;
using BookTaxi.Common2.Constants;
using BookTaxiEntyties.Entyties;
using BookTaxi.Services.JwtService;
using BookTaxiEntyties.Contracts;
using BookTaxi.Services.Extensions;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookTaxi.Services.SMS;
using BookTaxiEntyties.Repositiries;
using BookTaxi.Common2.DTOs;

namespace BookTaxi.Services.Api;

public class ClientService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;
    private readonly SmsService _smsService;
    private readonly ISmsRepository _smsRepository;
    //private readonly MemoryCache _memoryCache;

    public ClientService(
        IUserRepository userRepository,
        JwtTokenService jwtTokenService,
        SmsService smsService,
        ISmsRepository smsRepository)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _smsService = smsService;
        _smsRepository = smsRepository;
    }

    public async Task<int> ClientRegister(CreateClientModel model)
    {
        if (await CheckForExist(model.PhoneNumber))
        {
            throw new ArgumentException("User is already registered. Please log in!");
        }
        User user = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Role = Role.ClientRole,
        };

        await _userRepository.AddUser(user);
        var code = await _smsService.GenerateCode(model.PhoneNumber);
        return code;
    }

    public async Task<Tuple<int, int>> VerifyRegister(SmsModel model)
    {
        if (await CheckForExist(model.PhoneNumber) == false)
        {
            throw new Exception("User not found in base");
        }
        var verifyResult = await _smsService.VerifyCode(model.PhoneNumber, model.Code);
        var sms = await _smsRepository.GetByPhoneNumber(model.PhoneNumber);
        if (sms == null)
        {
            throw new Exception("Not found");
        }
        await _smsRepository.Update(sms);
        return new(sms.Id, sms.Code);
    }

    public async Task<int> ClientLogin(SmsLogInModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        var sms = await _smsRepository.GetByPhoneNumber(model.PhoneNumber);
        if (sms == null)
        {
            throw new InvalidOperationException("User phone is invalid.");
        }

        var code = await _smsService.GenerateCode(model.PhoneNumber);
        return code;
    }

    public async Task<Tuple<int, int>> VerifyLogin(SmsModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        // Проверяем, существует ли пользователь с таким номером телефона
        if (!await CheckForExist(model.PhoneNumber))
        {
            throw new InvalidOperationException("User not found in base.");
        }

        // Проверяем код подтверждения
        var verifyResult = await _smsService.VerifyCode(model.PhoneNumber, model.Code);

        // Получаем запись SMS по номеру телефона
        var sms = await _smsRepository.GetByPhoneNumber(model.PhoneNumber);
        if (sms == null)
        {
            throw new InvalidOperationException("SMS record not found.");
        }

        // Обновляем запись SMS (например, помечаем код как использованный)
        await _smsRepository.Update(sms);

        // Возвращаем ID и код подтверждения
        return new Tuple<int, int>(sms.Id, sms.Code);
    }

    public async Task<UserDto> ClientProfile(Guid Id)
    {
        var user = await _userRepository.GetUserById(Id);
        return user.ParseToDto();
    }

    public async Task<List<UserDto>> GetListDriver()
    {
        var users = await _userRepository.GetByRole(Role.DriverRole);
        return users.ParseToDtos();
    }

    private async Task<bool> CheckForExist(string phoneNumber)
    {
        var user = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        return user is not null;
    }

    //private static bool IsValidPhoneNumber(string phone)
    //{
    //    string pattern = @"^\+998\d{9}$";
    //    return Regex.IsMatch(phone, pattern);
    //}
}
