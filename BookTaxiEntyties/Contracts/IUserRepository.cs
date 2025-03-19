using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface IUserRepository
{
    Task Update(User entity);
    Task<User> Delete(User entity);
    Task AddUser(User entity);
    Task<List<User>> GetAllUsers();
    Task<User> GetUserById(Guid Id);
    Task<List<User>> GetByRole(string role);
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserByPhoneNumber(string phoneNumber);
}