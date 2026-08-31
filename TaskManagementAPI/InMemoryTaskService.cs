
namespace TaskManagementAPI
{
    public class InMemoryTaskService : ITaskService
    {
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
