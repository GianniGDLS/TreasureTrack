using Microsoft.Extensions.DependencyInjection;
using Mollie.Api.Client;
using Mollie.Api.Client.Abstract;
using TreasureTrack.Business.Workflows;
using TreasureTrack.Business.Workflows.Interfaces;
using TreasureTrack.Data.Managers;
using TreasureTrack.Data.Managers.Interfaces;

namespace TreasureTrack.Infrastructure.Setup
{
    public static class StartupExtensions
    {
        public static void RegisterManagers(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ILogManager, LogManager>();
        }

        public static void RegisterWorkflows(this IServiceCollection services)
        {
            services.AddScoped<IUserWorkflow, UserWorkflow>();
            services.AddScoped<IPaymentWorkflow, PaymentWorkflow>();
        }

        public static void RegisterClients(this IServiceCollection services)
        {
            services.AddSingleton<IPaymentClient>(x => new PaymentClient("live_D8yVNhvFJNM5kUrQq72dMT2q56Wud4"));
        }
    }
}