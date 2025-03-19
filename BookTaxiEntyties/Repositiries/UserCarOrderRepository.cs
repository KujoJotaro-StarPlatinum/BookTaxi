using Microsoft.EntityFrameworkCore;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Repositiries;

public class UserCarOrderRepository : IUserCarOrderRepository
{
    private readonly BookTaxiDbContext _context;
    public UserCarOrderRepository(BookTaxiDbContext context)
    {
        _context = context;
    }
    public UserCarsOrders AddUserCarOrder(UserCarsOrders entity)
    {
        _context.UserCarOrders.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public async Task<UserCarsOrders> Delete(UserCarsOrders entity)
    {
        _context.UserCarOrders.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<UserCarsOrders>> GetAllOrders()
    {
        var orders = await _context.UserCarOrders.AsNoTracking().ToListAsync();
        return orders;
    }

    public async Task<UserCarsOrders> GetByFromLocation(string fromLocation)
    {
        var order = await _context.UserCarOrders.SingleOrDefaultAsync(u => u.FromLocation == fromLocation);
        if (order is null)
        {
            throw new Exception("From Location not found");
        }
        return order;
    }

    public async Task<UserCarsOrders> GetById(int id)
    {
        var order = await _context.UserCarOrders.SingleOrDefaultAsync(u => u.Id == id);
        if (order is null)
        {
            throw new Exception("Id not found");
        }
        return order;
    }

    public async Task<UserCarsOrders> GetByPrice(string price)
    {
        var order = await _context.UserCarOrders.SingleOrDefaultAsync(u => u.Price == price);
        if (order is null)
        {
            throw new Exception("Price not found");
        }
        return order;
    }

    public async Task<UserCarsOrders> GetByToLocation(string toLocation)
    {
        var order = await _context.UserCarOrders.SingleOrDefaultAsync(u => u.ToLocation == toLocation);
        if (order is null)
        {
            throw new Exception("To Location not found");
        }
        return order;
    }

    public async Task<UserCarsOrders> Update(UserCarsOrders entity)
    {
        _context.UserCarOrders.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}