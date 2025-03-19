namespace BookTaxi.Common2.Models.CarModels;

public class AddCarModel
{
    public string Model { get; set; }
    public string CarName { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public Guid UserId { get; set; }
}