using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BookTaxi.Common2.Constants;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Entyties;

namespace Taxi.Data.Repositories;
public class SmsRepository : ISmsRepository
{
    private readonly BookTaxiDbContext _context;

    public SmsRepository(BookTaxiDbContext context)
    {
        _context = context;
    }

    public async Task AddSms(Sms sms)
    {
        _context.Sms.Add(sms);
        await _context.SaveChangesAsync();
    }

    public async Task<Sms> DeteleSms(Sms sms)
    {
        _context.Sms.Remove(sms);
        await _context.SaveChangesAsync();
        return sms;
    }


    public async Task<Sms> GetById(int Id)
    {
        var sms = await _context.Sms.FirstOrDefaultAsync(s => s.Id == Id);
        if (sms is null)
        {
            throw new Exception("This Id not found");
        }
        return sms;
    }

    public async Task<Sms> GetUserByPhoneNumber(string phoneNumber)
    {
        var sms = await _context.Sms.FirstOrDefaultAsync(_ => _.PhoneNumber == phoneNumber);
        if (sms is null)
        {
            throw new Exception("Phone number not found");
        }
        return sms;
    }

    public async Task<Sms> GetSmsCodePhoneNumber(string phoneNumber, int code)
    {
        var userSms = await _context.Sms.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Code == code);
        if (userSms is null)
        {
            throw new Exception("Id not found");
        }
        return userSms;
    }

    public async Task<Sms> GetSmsId(string phoneNumber, int code)
    {
        var sms = await _context.Sms.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Code == code && u.IsExpired == false);
        if (sms is null)
        {
            return null;
        }
        return sms;
    }

    public async Task<Sms> Update(Sms sms)
    {
        _context.Sms.Update(sms);
        await _context.SaveChangesAsync();
        return sms;
    }
}