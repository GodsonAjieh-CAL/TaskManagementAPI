
namespace TaskManagementAPI
{
    public class InMemoryTaskService : ITaskService
    {
        public Guid InstanceId { get; set; } = Guid.NewGuid();
        public Task<List<string>> GetTask()
        {
            List<string> strings = new List<string>
            {
                "database","cpu","api","nservicebus"
            };

            return Task.FromResult(strings);
        }

        /*public Task<List<string>> GetUsers()
        {
            throw new NotImplementedException();

        }*/
    }
}
