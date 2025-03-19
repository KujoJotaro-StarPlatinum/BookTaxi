using BookTaxiEntyties.Entyties;
using Microsoft.EntityFrameworkCore;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;

namespace BookTaxiEntyties.Repositiries;

public class UserCarRepository : IUserCarRepository
{
    private readonly BookTaxiDbContext _context;
    public UserCarRepository(BookTaxiDbContext context)
    {
        _context = context;
    }
    public UserCars AddUserCar(UserCars entity)
    {
        _context.UserCars.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public async Task<UserCars> Delete(UserCars entity)
    {
        _context.UserCars.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<UserCars>> GetAllUserCars()
    {
        var userCars = await _context.UserCars.AsNoTracking().ToListAsync();
        return userCars;
    }

    public async Task<UserCars> GetUserCarById(Guid Id)
    {
        var car = await _context.UserCars.SingleOrDefaultAsync(u => u.Id == Id);
        if (car is null)
        {
            throw new Exception("Car not found");
        }
        return car;
    }

    public async Task<List<UserCars>> GetUserCarByOwnerId(Guid UserId)
    {
        var users = await _context.UserCars.Where(u => u.UserId == UserId).ToListAsync();
        if (users is null)
        {
            throw new Exception("User not found");
        }
        return users;
    }

    public async Task<UserCars> Update(UserCars entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}