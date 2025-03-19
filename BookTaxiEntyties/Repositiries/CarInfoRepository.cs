using Microsoft.EntityFrameworkCore;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Repositiries;

public class CarInfoRepository : ICarInfoRepository
{
    private readonly BookTaxiDbContext _context;
    public CarInfoRepository(BookTaxiDbContext context)
    {
        _context = context;
    }
    public async Task<CarInfo> AddCarInfo(CarInfo entity)
    {
        _context.CarInfos.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<CarInfo> Delete(CarInfo entity)
    {
        _context.CarInfos.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<CarInfo>> GetAllCarInfos()
    {
        var carInfos = await _context.CarInfos.AsNoTracking().ToListAsync();
        return carInfos;
    }

    public async Task<CarInfo> GetByIdCarInfo(Guid Id)
    {
        var carinfo = await _context.CarInfos.SingleOrDefaultAsync(u => u.Id == Id);
        if (carinfo is null)
        {
            throw new Exception("CarInfo Id not found");
        }
        return carinfo;
    }

    public async Task<CarInfo> Update(CarInfo entity)
    {
        _context.CarInfos.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}