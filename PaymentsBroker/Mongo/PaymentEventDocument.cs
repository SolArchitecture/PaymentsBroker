namespace PaymentsBroker.Mongo;

public class PaymentEventDocument
{
    public decimal Price { get; set; }
    public string PSP { get; set; }
    public string OrderId { get; set; }
    public DateTime Timestamp { get; set; }
}