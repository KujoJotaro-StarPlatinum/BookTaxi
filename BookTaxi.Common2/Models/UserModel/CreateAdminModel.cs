using System.ComponentModel.DataAnnotations;

namespace BookTaxi.Common2.Models.UserModel;

public class CreateAdminModel
{
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    public string PhoneNumber { get; set; }
}