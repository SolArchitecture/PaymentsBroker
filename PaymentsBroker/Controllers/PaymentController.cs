using Microsoft.AspNetCore.Mvc;
using PaymentsBroker.Repository;

namespace PaymentsBroker.Controllers;

[ApiController]
[Route("api/")]
public class PaymentController : ControllerBase
{
    private readonly PaymentRepository _paymentRepository;
    
    public PaymentController(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    [HttpGet("payment")]
    public async Task<IActionResult> GetPaymentEvents()
    {
        var payments = await _paymentRepository.GetAllPayments();

        return Ok(payments);
    }
}