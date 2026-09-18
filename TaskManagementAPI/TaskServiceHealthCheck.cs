using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TaskManagementAPI
{
    public class TaskServiceHealthCheck : IHealthCheck
    {
        private readonly ITaskService _taskService;
        public TaskServiceHealthCheck(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _taskService.GetTask();
                return  HealthCheckResult.Healthy("Task service is responding");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Task service failed", ex);
            }
        }
    }
}
