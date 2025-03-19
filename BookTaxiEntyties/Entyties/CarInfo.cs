namespace BookTaxiEntyties.Entyties;

public class CarInfo
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public byte? SeatCounts { get; set; }
    public SeatInfo? SeatInfos { get; set; }
    public Cars? Car { get; set; }
    public Guid CarId { get; set; }
}