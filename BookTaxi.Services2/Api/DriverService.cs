using BookTaxi.Common2.Constants;
using BookTaxi.Common2.DTOs;
using BookTaxi.Common2.Models.UserModel;
using BookTaxi.Services.Extensions;
using BookTaxi.Services.JwtService;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace BookTaxi.Services.Api;

public class DriverService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;
    private readonly ILogger<DriverService> _logger;

    public DriverService(
        IUserRepository userRepository,
        JwtTokenService jwtTokenService,
        ILogger<DriverService> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<User> DriverRegister(CreateDriverModel model)
    {
        if (await CheckForExistUsername(model.UserName))
        {
            throw new InvalidOperationException("User already exists with this username.");
        }

        if (await CheckForExistPhoneNumber(model.PhoneNumber))
        {
            throw new InvalidOperationException("User already exists with this phone number.");
        }
        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Role = Role.DriverRole,
            PhoneNumber = model.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
        };

        var passwordHasher = new PasswordHasher<User>();
        user.HashPassword = passwordHasher.HashPassword(user, model.Password);

        try
        {
            await _userRepository.AddUser(user);

            _logger.LogInformation("Driver registered successfully: {Username}, Phone: {PhoneNumber}", model.UserName, model.PhoneNumber);

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering the admin: {Username}", model.UserName);
            throw new InvalidOperationException("An error occurred while registering the user", ex);
        }
    }

    public async Task<string> DriverLogin(LogInDriverModel model)
    {
        try
        {
            var user = await _userRepository.GetUserByUsername(model.Username);
            if (user is null)
            {
                throw new Exception("User is invalid");
            }
            if (user.Role != Role.DriverRole)
            {
                throw new Exception("User role is invalid");
            }
            var result = new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.HashPassword, model.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid password");
            }
            var token = _jwtTokenService.GenerateToken(user);
            return token;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to login {ex.Message}");
        }
    }

    public async Task<UserDto> DriverProfile(Guid Id)
    {
        var user = await _userRepository.GetUserById(Id);
        if (user.Role != Role.DriverRole)
        {
            throw new Exception("You are providing an incorrect ID. You are not allowed to search for this ID.");
        }
        return user.ParseToDto();
    }

    private async Task<bool> CheckForExist(string username)
    {
        var user = await _userRepository.GetUserByUsername(username);
        if (user is not null)
        {
            return false;
        }
        return true;
    }

    private async Task<bool> CheckForExistPhoneNumber(string phoneNumber)
    {
        var user = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        if (user is not null)
        {
            return false;
        }
        return true;
    }
    private async Task<bool> CheckForExistUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username cannot be null or empty", nameof(username));
        }

        // Получаем пользователя по имени
        var user = await _userRepository.GetUserByUsername(username);

        // Возвращаем true, если пользователь существует, иначе false
        return user != null;
    }
}