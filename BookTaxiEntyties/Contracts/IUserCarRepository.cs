using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface IUserCarRepository
{
    Task<UserCars> Update(UserCars entity);
    Task<UserCars> Delete(UserCars entity);
    UserCars AddUserCar(UserCars entity);
    Task<List<UserCars>> GetAllUserCars();
    Task<UserCars> GetUserCarById(Guid Id);
    Task<List<UserCars>> GetUserCarByOwnerId(Guid UserId);
}