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

public class AdminService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;
    private readonly ILogger<AdminService> _logger;

    public AdminService(
        IUserRepository userRepository,
        JwtTokenService jwtTokenService,
        ILogger<AdminService> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<string> AdminRegister(CreateAdminModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

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
            Role = Role.AdminRole,
            PhoneNumber = model.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
        };

        var passwordHasher = new PasswordHasher<User>();
        user.HashPassword = passwordHasher.HashPassword(user, model.Password);

        try
        {
            await _userRepository.AddUser(user);

            _logger.LogInformation("Admin registered successfully: {Username}", model.UserName);
            return "Registered successfully";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering the admin: {Username}", model.UserName);
            throw new InvalidOperationException("An error occurred while registering the user", ex);
        }
    }

    public async Task<string> AdminLogIn(CreateAdminModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var user = await _userRepository.GetUserByUsername(model.UserName);
        if (user == null)
        {
            throw new ArgumentException("Username is invalid");
        }

        if (user.Role != Role.AdminRole)
        {
            throw new InvalidOperationException("User is not an admin");
        }

        if (string.IsNullOrEmpty(user.HashPassword))
        {
            throw new InvalidOperationException("Password can't be empty");
        }

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.HashPassword, model.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new ArgumentException("Invalid password");
        }
        try
        {
            var token = _jwtTokenService.GenerateToken(user);
            _logger.LogInformation("Admin logged in successfully: {Username}", model.UserName);
            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating token for user: {Username}", model.UserName);
            throw;
        }
    }

    public async Task<UserDto> AdminProfile(Guid id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        return user.ParseToDto();
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return users.Select(u => u.ParseToDto()).ToList();
    }

    private async Task<bool> CheckForExistUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username cannot be null or empty", nameof(username));
        }

        var user = await _userRepository.GetUserByUsername(username);
        return user != null;
    }

    private async Task<bool> CheckForExistPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));
        }

        var user = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        return user != null;
    }
}