using Microsoft.EntityFrameworkCore;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Repositiries;

public class DestinationRepository : IDestinationRepository
{
    private readonly BookTaxiDbContext _context;
    public DestinationRepository(BookTaxiDbContext context)
    {
        _context = context;
    }
    public async Task AddDestination(Destination entity)
    {
        _context.Destinations.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Destination> Delete(Destination entity)
    {
        _context.Destinations.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Destination>> GetAllDestination()
    {
        var destination = await _context.Destinations.AsNoTracking().ToListAsync();
        return destination;
    }

    public async Task<Destination> GetByFromWhere(string fromWhere)
    {
        var destination = await _context.Destinations.SingleOrDefaultAsync(u => u.FromWhere == fromWhere);
        if (destination is null)
        {
            throw new Exception("FromWhere not found");
        }
        return destination;
    }

    public async Task<Destination> GetByToWhere(string toWhere)
    {
        var destination = await _context.Destinations.SingleOrDefaultAsync(u => u.ToWhere == toWhere);
        if (destination is null)
        {
            throw new Exception("ToWhere not found");
        }
        return destination;
    }

    public async Task<Destination> Update(Destination entity)
    {
        _context.Destinations.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}