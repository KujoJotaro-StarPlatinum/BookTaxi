using BookTaxi.Contracts;
using BookTaxiEntyties.BookTaxiDbContext;
using BookTaxiEntyties.Entyties;
using Microsoft.EntityFrameworkCore;

namespace GoTaxi.Api.Repositories;
public class CarRepository : ICarRepository
{
    private readonly BookTaxiDbContext _context;
    public CarRepository(BookTaxiDbContext context)
    {
        _context = context;
    }
    public async Task<Cars> Add(Cars entity)
    {
        _context.Cars.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Cars> Delete(Cars entity)
    {
        _context.Cars.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Cars>> GetAllCars()
    {
        var cars = await _context.Cars.AsNoTracking().ToListAsync();
        return cars;
    }

    public async Task<Cars> GetCarById(Cars cars)
    {
        var car = await _context.Cars.SingleOrDefaultAsync(u => u.Id == cars.Id);
        if (car is null)
        {
            throw new NotImplementedException();
        }
        return car;
    }

    public async Task<Cars> GetCarsOwner(Cars cars)
    {
        var car = await _context.Cars.SingleOrDefaultAsync(x => x.UserId == cars.UserId);
        if (car is null)
        {
            throw new NotImplementedException();
        }
        return car;
    }

    public async Task<Cars> Update(Cars entity)
    {
        _context.Cars.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}