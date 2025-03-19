using System.ComponentModel.DataAnnotations;

namespace BookTaxi.Common2.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? PasswordHash { get; set; }
    public string? Desciption { get; set; }
    [Required]
    public string Role { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    public DateTime DreateAt = DateTime.UtcNow;
    public List<UserCarDto>? UserCars { get; set; }
    public List<UserCarsOrdersDto>? OrdersDtos { get; set; }
}