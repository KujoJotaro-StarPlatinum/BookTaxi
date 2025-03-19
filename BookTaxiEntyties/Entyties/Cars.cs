using System.Text.Json.Serialization;

namespace BookTaxiEntyties.Entyties;

public class Cars
{
    public Guid Id { get; set; }
    public string? Model { get; set; }
    public string? CarName { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public List<CarInfo>? CarInfos { get; set; }
    public UserCars? UserCars { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public Destination? Destination { get; set; }
    public Guid DestinationId { get; set; }
}