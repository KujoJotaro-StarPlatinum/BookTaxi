namespace BookTaxi.Common2.DTOs;

public class UserCarDto
{
    public Guid Id { get; set; }
    public bool IsOwner { get; set; }
    public UserDto? User { get; set; }
    public Guid UserId { get; set; }
    public CarsDto Car { get; set; }
    public Guid CarId { get; set; }
}