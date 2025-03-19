using Microsoft.EntityFrameworkCore;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Repositiries;

public class CarRepository : ICarRepository
{
    private readonly BookTaxiDbContext _context;
    public CarRepository(BookTaxiDbContext context)
    {
        _context = context;
    }
    public async Task<Cars> AddCar(Cars entity)
    {
        await _context.Cars.AddAsync(entity);
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

    public async Task<Cars> GetCarById(Guid id)
    {
        var car = await _context.Cars.SingleOrDefaultAsync(c => c.Id == id);
        if (car is null)
        {
            throw new Exception("Car not found");
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