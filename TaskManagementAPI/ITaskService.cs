namespace TaskManagementAPI
{
    public interface ITaskService
    {
        //Task <List<string>> GetUsers();

        public Guid InstanceId { get; }

        Task<List<string>> GetTask();

        Task<string> GetTaskById(int id);

        Task<string> AddTask(string name);

        Task<string> UpdateId(int id, string name);

        Task<string> DeleteById(int id);
   }
}
