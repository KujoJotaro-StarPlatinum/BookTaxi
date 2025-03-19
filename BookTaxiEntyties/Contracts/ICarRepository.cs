using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface ICarRepository
{
    Task<Cars> Update(Cars entity);
    Task<Cars> Delete(Cars entity);
    Task<Cars> AddCar(Cars entity);
    Task<List<Cars>> GetAllCars();
    Task<Cars> GetCarById(Guid id);
}