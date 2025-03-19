namespace BookTaxi.Common2.Models.DestinationModels;

public class AddDestinationModel
{
    public string? FromWhere { get; set; }
    public string? ToWhere { get; set; }
    public string? DestinationPrice { get; set; }
    public string? Description { get; set; }
    public Guid CarId { get; set; }
}