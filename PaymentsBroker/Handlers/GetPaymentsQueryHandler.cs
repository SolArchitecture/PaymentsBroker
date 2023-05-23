using PaymentsBroker.Mongo;
using PaymentsBroker.Queries;
using PaymentsBroker.Repository;

namespace PaymentsBroker.Handlers;

public class GetPaymentsQueryHandler : IQueryHandler<GetPaymentQuery, List<PaymentEventDocument>>
{
    private readonly PaymentRepository _paymentRepository;
    
    public GetPaymentsQueryHandler(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<List<PaymentEventDocument>> Handle(GetPaymentQuery query, CancellationToken cancellationToken)
    {
        return await _paymentRepository.GetAllPayments();
    }
}