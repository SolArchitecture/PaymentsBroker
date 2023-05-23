using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentsBroker.Mongo;

namespace PaymentsBroker.Repository;

public class PaymentRepository
{
    private readonly IMongoCollection<PaymentEventDocument> _paymentCollection;

    public PaymentRepository(IOptions<PaymentDatabaseSettings> paymentDBSettings)
    {
        MongoClient client = new MongoClient(paymentDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(paymentDBSettings.Value.DatabaseName);

        _paymentCollection = database.GetCollection<PaymentEventDocument>(paymentDBSettings.Value.CollectionName);
    }

    public async Task<List<PaymentEventDocument>> GetAllPayments()
    {
        var sort = Builders<PaymentEventDocument>.Sort.Ascending(p => p.Timestamp);

        var result = await _paymentCollection.Find(Builders<PaymentEventDocument>.Filter.Empty)
            .Sort(sort)
            .ToListAsync();

        return result;
    }

    public async Task AddPayment(PaymentEventDocument pd)
    {
        await _paymentCollection.InsertOneAsync(pd);
    }
}