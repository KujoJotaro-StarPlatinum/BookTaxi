using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface ISmsRepository
{
    Task AddSms(Sms sms);
    Task<Sms> GetSmsId(string phoneNumber, int code);
    Task<Sms> DeteleSms(Sms sms);
    Task<Sms> GetById(int Id);
    Task<Sms> Update(Sms sms);
    Task<Sms> GetByPhoneNumber(string phoneNumber);
}