using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface IDestinationRepository
{
    Task<Destination> Update(Destination entity);
    Task<Destination> Delete(Destination entity);
    Task AddDestination(Destination entity);
    Task<List<Destination>> GetAllDestination();
    Task<Destination> GetByFromWhere(string fromWhere);
    Task<Destination> GetByToWhere(string toWhere);
}