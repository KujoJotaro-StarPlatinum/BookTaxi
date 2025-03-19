namespace BookTaxi.Common2.DTOs;

public class SmsDto
{
    public string PhoneNumber { get; set; }
    public int Code { get; set; }
    public bool IsExpired { get; set; }
    public DateTime ExpirationTime { get; set; }
}