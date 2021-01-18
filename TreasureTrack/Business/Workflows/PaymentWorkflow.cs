using Mollie.Api.Client;
using Mollie.Api.Client.Abstract;
using Mollie.Api.Models;
using Mollie.Api.Models.Payment;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;
using System;
using System.Threading.Tasks;
using TreasureTrack.Business.Workflows.Interfaces;
using TreasureTrack.Data.Managers.Interfaces;

namespace TreasureTrack.Business.Workflows
{
    public class PaymentWorkflow: IPaymentWorkflow
    {
        private readonly IUserManager _userManager;
        private readonly ILogManager _logManager;
        private readonly IPaymentClient _paymentClient;

        public PaymentWorkflow(IUserManager userManager, ILogManager logManager, IPaymentClient paymentClient)
        {
            _userManager = userManager;
            _logManager = logManager;
            _paymentClient = paymentClient;
        }

        public async Task<PaymentResponse> RequestTestPaymentAsync(int userId)
        {
            var client = new PaymentClient("test_E2S2gNGVfwyKHQemPs7Ab2CnVqygKR");
            var paymentResponse = await client.CreatePaymentAsync(new PaymentRequest
            {
                Amount = new Amount(Currency.EUR, 0.02m),
                Description = "betaling voor Treasure Track",
                Method = PaymentMethod.Bancontact,
                RedirectUrl = "https://treasuretrack.be/game",
            }, true);
            await _userManager.SavePaymentIdForUserAsync(userId, paymentResponse.Id);
            return paymentResponse;
        }

        public async Task<PaymentResponse> RequestPaymentAsync(int userId)
        {
            PaymentResponse paymentResponse;
            try
            {
                paymentResponse = await _paymentClient.CreatePaymentAsync(new PaymentRequest
                {
                    Amount = new Amount(Currency.EUR, 10.00m),
                    Description = "betaling voor Treasure Track",
                    Method = PaymentMethod.Bancontact,
                    RedirectUrl = "https://treasuretrack.be/game",
                }, true);
                await _userManager.SavePaymentIdForUserAsync(userId, paymentResponse.Id);
            }
            catch (Exception e)
            {
                await _logManager.WriteLogAsync(e.Message, userId);
                throw;
            }

            return paymentResponse;
        }

        public async Task<string> CheckPaymentStatusTestAsync(int userId)
        {
            var client = new PaymentClient("test_E2S2gNGVfwyKHQemPs7Ab2CnVqygKR");
            PaymentResponse paymentResponse;
            try
            {
                var user = await _userManager.GetUserAsync(userId);
                paymentResponse = await client.GetPaymentAsync(user.PaymentId);
                if (paymentResponse.Status == PaymentStatus.Paid)
                    await _userManager.ActivateRegistrationAsync(userId);
            }
            catch (Exception e)
            {
                await _logManager.WriteLogAsync(e.Message, userId);
                throw;
            }
            return paymentResponse.Status;
        }

        public async Task<string> CheckPaymentStatusAsync(int userId)
        {
            PaymentResponse paymentResponse;
            try
            {
                var user = await _userManager.GetUserAsync(userId);
                paymentResponse = await _paymentClient.GetPaymentAsync(user.PaymentId);
                if (paymentResponse.Status == PaymentStatus.Paid)
                    await _userManager.ActivateRegistrationAsync(userId);
            }
            catch (Exception e)
            {
                await _logManager.WriteLogAsync(e.Message, userId);
                throw;
            }
            return paymentResponse.Status;
        }
    }
}
