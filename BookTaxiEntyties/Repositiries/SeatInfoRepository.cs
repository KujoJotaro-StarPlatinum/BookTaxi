using Microsoft.EntityFrameworkCore;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Repositiries;

public class SeatInfoRepository : ISeatInfoRepository
{
    private readonly BookTaxiDbContext _context;
    public SeatInfoRepository(BookTaxiDbContext context)
    {
        _context = context;
    }

    public async Task<SeatInfo> AddSeatInfo(SeatInfo entity)
    {
        _context.SeatInfos.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<SeatInfo> Delete(SeatInfo entity)
    {
        _context.SeatInfos.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<SeatInfo>> GetAllSeatInfo()
    {
        var seatInfo = await _context.SeatInfos.AsNoTracking().ToListAsync();
        return seatInfo;
    }

    public async Task<SeatInfo> GetByDiscount(decimal discount)
    {
        var seatInfo = await _context.SeatInfos.SingleOrDefaultAsync(u => u.Discount == discount);
        if (seatInfo is null)
        {
            throw new Exception("Discount not found");
        }
        return seatInfo;
    }

    public async Task<SeatInfo> GetBySeatName(string seatName)
    {
        var seatInfo = await _context.SeatInfos.SingleOrDefaultAsync(u => u.SeatName == seatName);
        if (seatInfo is null)
        {
            throw new Exception("SeatName not found");
        }
        return seatInfo;
    }

    public async Task<SeatInfo> Update(SeatInfo entity)
    {
        _context.SeatInfos.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}