using Microsoft.AspNetCore.Mvc;
using PaymentsBroker.Handlers;
using PaymentsBroker.Mongo;
using PaymentsBroker.Queries;
using PaymentsBroker.Repository;

namespace PaymentsBroker.Controllers;

[ApiController]
[Route("api/")]
public class PaymentController : ControllerBase
{
    private readonly IQueryHandler<GetPaymentQuery, List<PaymentEventDocument>> _getPaymentsQuery;

    public PaymentController(IQueryHandler<GetPaymentQuery, List<PaymentEventDocument>> getPaymentsQuery)
    {
        _getPaymentsQuery = getPaymentsQuery;
    }

    [HttpGet("payment")]
    public async Task<IActionResult> GetPaymentEvents()
    {
        GetPaymentQuery pq = new GetPaymentQuery();
        var payments = await _getPaymentsQuery.Handle(pq, CancellationToken.None);
        return Ok(payments);
    }
}