using BookTaxiEntyties.Entyties;

namespace BookTaxi.Contracts;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User> GetAllCLients(Guid id);
    Task<User> GetAllOwners(Guid id);
    Task<User> GetUserById(Guid Id);
    Task<User?> GetUserByUsername(string username);

}
