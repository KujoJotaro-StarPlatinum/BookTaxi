namespace BookTaxiEntyties.Entyties;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? HashPassword { get; set; }
    public string? Description { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt = DateTime.UtcNow;
    public List<UserCars>? UserCars { get; set; }
    public List<UserCarsOrders>? Orders { get; set; }
}