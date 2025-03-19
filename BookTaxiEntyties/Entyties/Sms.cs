namespace BookTaxiEntyties.Entyties;

public class Sms
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public int Code { get; set; }
    public bool IsExpired { get; set; }
    public DateTime ExpirationTime { get; set; }
}