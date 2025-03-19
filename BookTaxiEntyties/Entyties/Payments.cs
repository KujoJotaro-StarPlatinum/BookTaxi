namespace BookTaxiEntyties.Entyties;

public class Payments
{
    public int Id { get; set; }
    public string Amount { get; set; }
    public DateTime PaymentDate = DateTime.UtcNow;
    public string Status { get; set; }
    public string PaymentMethod { get; set; }
    public UserCarsOrders? CarOrder { get; set; }
    public Guid OrderId { get; set; }
}