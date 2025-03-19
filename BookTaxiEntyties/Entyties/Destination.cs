namespace BookTaxiEntyties.Entyties;

public class Destination
{
    public Guid Id { get; set; }
    public string FromWhere { get; set; }
    public string ToWhere { get; set; }
    public Cars? Car { get; set; }
    public Guid CarId { get; set; }
}