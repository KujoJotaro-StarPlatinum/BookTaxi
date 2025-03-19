namespace BookTaxi.Common2.DTOs;
public class PaymentsDto
{
    public string Amount { get; set; }
    public DateTime PaymentDate = DateTime.UtcNow;
    public string Status { get; set; }
    public string PaymentMethod { get; set; }

    public UserCarsOrdersDto? CarOrder { get; set; }
    public Guid OrderId { get; set; }
}
