namespace PaymentsBroker.Messages;

public class PaymentMessage
{
    public decimal Price { get; set; }
    public string PSP { get; set; }
    public string OrderId { get; set; }
    public string Email { get; set; }
    public DateTime Timestamp { get; set; }
}