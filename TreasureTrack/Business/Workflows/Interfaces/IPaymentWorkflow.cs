using Mollie.Api.Models.Payment.Response;
using System.Threading.Tasks;

namespace TreasureTrack.Business.Workflows.Interfaces
{
    public interface IPaymentWorkflow
    {
        Task<PaymentResponse> RequestTestPaymentAsync(int userId);
        Task<PaymentResponse> RequestPaymentAsync(int userId);
        Task<string> CheckPaymentStatusTestAsync(int userId);
        Task<string> CheckPaymentStatusAsync(int userId);
    }
}