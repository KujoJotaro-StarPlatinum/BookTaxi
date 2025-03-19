namespace BookTaxi.Common2.Models.UserModel;

public class ClientAllDriversModel
{
    public Guid Id { get; set; }
    public string? Model { get; set; }
    public string? CarName { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
}