using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface ISeatInfoRepository
{
    Task<SeatInfo> Update(SeatInfo entity);
    Task<SeatInfo> Delete(SeatInfo entity);
    Task<SeatInfo> AddSeatInfo(SeatInfo entity);
    Task<List<SeatInfo>> GetAllSeatInfo();
    Task<SeatInfo> GetBySeatName(string seatName);
    Task<SeatInfo> GetByDiscount(decimal discount);
}