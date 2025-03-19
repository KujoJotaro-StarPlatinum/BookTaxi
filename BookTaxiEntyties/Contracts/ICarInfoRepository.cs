using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface ICarInfoRepository
{
    Task<CarInfo> Update(CarInfo entity);
    Task<CarInfo> Delete(CarInfo entity);
    Task<CarInfo> AddCarInfo(CarInfo entity);
    Task<List<CarInfo>> GetAllCarInfos();
    Task<CarInfo> GetByIdCarInfo(Guid Id);
}