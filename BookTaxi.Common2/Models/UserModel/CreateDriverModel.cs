namespace BookTaxi.Common2.Models.UserModel;

public class CreateDriverModel
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}