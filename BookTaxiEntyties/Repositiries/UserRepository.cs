using Microsoft.EntityFrameworkCore;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Repositiries;

public class UserRepository : IUserRepository
{
    private readonly BookTaxiDbContext _context;

    public UserRepository(BookTaxiDbContext context)
    {
        _context = context;
    }

    public async Task AddUser(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<User> Delete(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<User>> GetByRole(string role)
    {
        return await _context.Users.Where(u => u.Role == role).ToListAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User> GetUserById(Guid id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        return user;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user is null)
        {
            return null;
        }
        return user;
    }

    public async Task Update(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserByPhoneNumber(string phoneNumber)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        if (user is null)
        {
            return null;
        }
        return user;
    }
}