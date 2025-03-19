using BookTaxi.Common2.DTOs;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Entyties;

namespace BookTaxi.Services.Api;

public class CarInfoService
{
    private readonly ICarInfoRepository _repository;
    public CarInfoService(ICarInfoRepository repository)
    {
        _repository = repository;
    }

    public async Task<CarInfo> AddCarInfo(CarInfoDto model)
    {
        CarInfo carInfo = new CarInfo()
        {
            Id = Guid.NewGuid(),
            CarId = model.CarId,
            Text = model.Text,
            SeatCounts = model.SeatCounts,
        };
        await _repository.AddCarInfo(carInfo);
        return carInfo;
    }

    public async Task<List<CarInfo>> GetAllCarInfos()
    {
        try
        {
            var infos = await _repository.GetAllCarInfos();
            return infos;
        }
        catch (Exception ex)
        {
            return new List<CarInfo>();
        }
    }
}