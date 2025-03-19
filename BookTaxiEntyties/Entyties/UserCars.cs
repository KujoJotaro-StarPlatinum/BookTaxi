using System.Text.Json.Serialization;

namespace BookTaxiEntyties.Entyties;

public class UserCars
{
    public Guid Id { get; set; }
    public bool IsOwner { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public Cars? Car { get; set; }
    public Guid CarId { get; set; }
}