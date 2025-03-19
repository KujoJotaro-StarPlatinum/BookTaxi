namespace BookTaxi.Common2.Models.PaymentModels;

public class PaymentAdd
{
    public string Amount { get; set; }
    public string Status { get; set; }
    public string PaymentMethod { get; set; }
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
}