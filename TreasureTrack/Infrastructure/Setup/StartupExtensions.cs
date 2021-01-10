using Microsoft.Extensions.DependencyInjection;
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
        }

        public static void RegisterWorkflows(this IServiceCollection services)
        {
            services.AddScoped<IUserWorkflow, UserWorkflow>();
        }
    }
}