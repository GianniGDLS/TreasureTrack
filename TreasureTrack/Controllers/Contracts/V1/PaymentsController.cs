using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TreasureTrack.Business.Workflows.Interfaces;

namespace TreasureTrack.Controllers.Contracts.V1
{
    [Route("payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentWorkflow _paymentWorkflow;

        public PaymentsController(IPaymentWorkflow paymentWorkflow)
        {
            _paymentWorkflow = paymentWorkflow;
        }

        [HttpGet("request-test")]
        public async Task<IActionResult> RequestTestPaymentAsync(int userId)
        {
            var result = await _paymentWorkflow.RequestTestPaymentAsync(userId);
            return Ok(result);
        }

        [HttpGet("request")]
        public async Task<IActionResult> RequestPaymentAsync(int userId)
        {
            var result = await _paymentWorkflow.RequestPaymentAsync(userId);
            return Ok(result);
        }

        [HttpGet("status-test")]
        public async Task<IActionResult> CheckPaymentStatusTestAsync(int userId)
        {
            var result = await _paymentWorkflow.CheckPaymentStatusTestAsync(userId);
            return Ok(result);
        }

        [HttpGet("status")]
        public async Task<IActionResult> CheckPaymentStatusAsync(int userId)
        {
            var result = await _paymentWorkflow.CheckPaymentStatusAsync(userId);
            return Ok(result);
        }
    }
}