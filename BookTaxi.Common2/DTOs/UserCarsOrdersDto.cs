namespace BookTaxi.Common2.DTOs;

public class UserCarsOrdersDto
{
    public string? FromLocation { get; set; }
    public string? ToLocation { get; set; }
    public string? Price { get; set; }
    public string? Place { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
}