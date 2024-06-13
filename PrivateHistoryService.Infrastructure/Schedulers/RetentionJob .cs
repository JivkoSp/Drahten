using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using Quartz;

namespace PrivateHistoryService.Infrastructure.Schedulers
{
    public class RetentionJob : IJob
    {
        private readonly ILogger<RetentionJob> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RetentionJob(ILogger<RetentionJob> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Retention job started.");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var userWriteService = scope.ServiceProvider.GetRequiredService<IUserWriteService>();

                var users = await userWriteService.GetUsersAsync();

                // Set RetentionUntil to 24 hours from now for users that does not have RetentionUntil.
                var usersToUpdate = users.Where(u => u.HasUserRetentionDateTime() == false).ToList();

                if(usersToUpdate.Any())
                {
                    foreach (var user in usersToUpdate)
                    {
                        user.SetUserRetentionDateTime(new UserRetentionUntil(DateTimeOffset.Now.AddHours(24)));
                    }

                    await userWriteService.UpdateUsersAsync(usersToUpdate);
                }

                // Delete users with expired RetentionUntil.
                var usersToDelete = users
                    .Where(x => x.IsUserRetentionDateTimeExpired(DateTimeOffset.Now)).ToList();

                await userWriteService.DeleteUsersAsync(usersToDelete);
            }

            _logger.LogInformation("Retention job completed.");
        }
    }
}
