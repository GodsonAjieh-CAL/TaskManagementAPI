using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
namespace TaskManagementAPI
{
    public class InMemoryTaskService : ITaskService
    {
        private readonly IOptions<TaskServiceOptions> _taskOptions;
        private readonly AppDbContext _context;
        public Guid InstanceId { get; set; } = Guid.NewGuid();

        public InMemoryTaskService(IOptions<TaskServiceOptions> taskOptions, AppDbContext context)
        {
            _taskOptions = taskOptions;
            _context = context;
        }
        public async Task<List<string>> GetTask()
        {
            //No longer in memory
            /*List<string> strings = new List<string>
            {
                "database","cpu","api","nservicebus"
            };

            var option = _taskOptions.Value.MaxTasksToReturn;
            var limitedTasks = strings.Take(option).ToList();*/

            var names = await _context.Tasks
                .Select(t => t.Name)
                .Take(_taskOptions.Value.MaxTasksToReturn)
                .ToListAsync();

            return names;
        }

        public async Task<string> GetTaskById(int id)
        {
            /*List<string> strings = new List<string>
            {
                "database","cpu","api","nservicebus"
            };

            if (id < 0 || id >= strings.Count)
            {
                return Task.FromResult<string>(null);
            }


            return Task.FromResult(strings[id]);*/

            var task = await _context.Tasks.FindAsync(id);
            return task?.Name;

        }

    }

    /*public Task<List<string>> GetUsers()
    {
        throw new NotImplementedException();

    }*/
}
