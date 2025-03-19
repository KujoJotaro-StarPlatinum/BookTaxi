using BookTaxi.Common2.Models.PaymentModels;
using BookTaxi.Services.Api;
using Microsoft.AspNetCore.Mvc;

namespace BookTaxi.Api2.Controllers;

[ApiController]
[Route("payment-action/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("payments")]
    public async Task<IActionResult> GetAllPayments()
    {
        try
        {
            var payments = await _paymentService.GetAllPayments();
            return Ok(payments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("payments/{payment_id}")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        try
        {
            var payment = await _paymentService.GetByIdPayment(id);
            return Ok(payment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost("add-payment")]
    public async Task<IActionResult> AddPayment(PaymentAdd model)
    {
        try
        {
            var payment = await _paymentService.AddPayment(model);
            return Ok(payment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
