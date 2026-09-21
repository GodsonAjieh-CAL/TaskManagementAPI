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

        public async Task<string> AddTask(string name)
        {
            var task = await _context.Tasks.AddAsync(new TaskItem { Name = name });
            await _context.SaveChangesAsync();
            return task.Entity.Name;

        }

        public async Task<string> UpdateId(int id, string name)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return null;
            }
            task.Name = name;
            await _context.SaveChangesAsync();
            return task.Name;
        }

        public async Task<string> DeleteById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if(task == null)
            {
                return null;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task.Name;
        }
    }

    /*public Task<List<string>> GetUsers()
    {
        throw new NotImplementedException();

    }*/
}
