using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface IUserCarOrderRepository
{
    Task<UserCarsOrders> Update(UserCarsOrders entity);
    Task<UserCarsOrders> Delete(UserCarsOrders entity);
    UserCarsOrders AddUserCarOrder(UserCarsOrders entity);
    Task<List<UserCarsOrders>> GetAllOrders();
    Task<UserCarsOrders> GetById(int id);
    Task<UserCarsOrders> GetByFromLocation(string fromLocation);
    Task<UserCarsOrders> GetByToLocation(string toLocation);
    Task<UserCarsOrders> GetByPrice(string price);
}