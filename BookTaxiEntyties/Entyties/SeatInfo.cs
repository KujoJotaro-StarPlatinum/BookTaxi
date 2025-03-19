namespace BookTaxiEntyties.Entyties;

public class SeatInfo
{
    public Guid Id { get; set; }
    public string? SeatName { get; set; }
    public decimal? Discount { get; set; }
    public Cars? Car { get; set; }
    public Guid CarId { get; set; }
}