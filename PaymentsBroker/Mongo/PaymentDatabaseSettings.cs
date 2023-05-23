namespace PaymentsBroker.Mongo;

public class PaymentDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CollectionName { get; set; } = null!;
    
    
    public PaymentDatabaseSettings()
    {
        ConnectionString = "mongodb://mongodb:27020";
        DatabaseName = "payments";
        CollectionName = "payment-events";
    }
}