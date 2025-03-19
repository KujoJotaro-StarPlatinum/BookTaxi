namespace BookTaxi.Common2.DTOs;

public class SeatInfoDto
{
    public Guid Id { get; set; }
    public string SeatName { get; set; }
    public decimal Discount { get; set; }
    public CarsDto? Car { get; set; }
    public Guid CarId { get; set; }
}
