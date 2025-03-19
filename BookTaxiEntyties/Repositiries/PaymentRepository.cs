using Microsoft.EntityFrameworkCore;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Repositiries;

public class PaymentRepository : IPaymentRepository
{
    private readonly BookTaxiDbContext _context;
    public PaymentRepository(BookTaxiDbContext context)
    {
        _context = context;
    }
    public async Task AddPayment(Payments entity)
    {
        _context.Paymets.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Payments> Delete(Payments entity)
    {
        _context.Paymets.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Payments> GetByPaymentDate(DateTime paymentDate)
    {
        var date = await _context.Paymets.SingleOrDefaultAsync(u => u.PaymentDate == paymentDate);
        if (date is null)
        {
            throw new Exception("Date not found");
        }
        return date;
    }

    public async Task<List<Payments>> GetAllPayments()
    {
        var payments = await _context.Paymets.AsNoTracking().ToListAsync();
        return payments;
    }

    public async Task<Payments> GetByAmount(string amount)
    {
        var payment = await _context.Paymets.SingleOrDefaultAsync(u => u.Amount == amount);
        if (payment is null)
        {
            throw new Exception($"Payment {amount}");
        }
        return payment;
    }

    public async Task<Payments> GetByStatus(string status)
    {
        var payment = await _context.Paymets.SingleOrDefaultAsync(u => u.Status == status);
        if (payment is null)
        {
            throw new Exception($"Payments : {status}");
        }
        return payment;
    }

    public async Task<Payments> Update(Payments entity)
    {
        _context.Paymets.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Payments> GetByIdPayment(int id)
    {
        var payment = await _context.Paymets.FirstOrDefaultAsync(u => u.Id == id);
        return payment;
    }
}