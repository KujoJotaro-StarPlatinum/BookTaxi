using BookTaxi.Common2.Constants;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;
using System.Security.Cryptography;

namespace BookTaxi.Services.SMS;

public class SmsService
{
    private readonly IUserRepository _userRepository;
    private readonly ISmsRepository _smsRepository;

    public SmsService(ISmsRepository smsRepository, IUserRepository userRepository)
    {
        _smsRepository = smsRepository;
        _userRepository = userRepository;
    }

    public async Task<int> GenerateCode(string phoneNumber)
    {
        var code = RandomNumberGenerator.GetInt32(100000, 999999);
        var newSms = new Sms()
        {
            PhoneNumber = phoneNumber,
            Code = code,
            IsExpired = false,
            ExpirationTime = DateTime.UtcNow.AddMinutes(2)
        };
        await _smsRepository.AddSms(newSms);

        return code;
    }

    public async Task<string> VerifyCode(string phoneNumber, int code)
    {
        var sms = await _smsRepository.GetSmsId(phoneNumber, code);

        if (sms == null)
        {
            throw new Exception("SMS not found or invalid");
        }

        if (sms.IsExpired)
        {
            throw new Exception("SMS code is expired");
        }

        return "Successfully verified";
    }

    public async Task<int> GenerateCodeLogin(string phoneNumber, int id)
    {
        var smsGet = await _smsRepository.GetById(id);
        var code = RandomNumberGenerator.GetInt32(100000, 999999);
        Sms sms = new()
        {
            PhoneNumber = phoneNumber,
            Code = smsGet.Code,
        };
        await _smsRepository.Update(sms);
        return code;
    }
}
