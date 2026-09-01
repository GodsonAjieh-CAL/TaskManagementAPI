namespace TaskManagementAPI
{
    public interface ITaskService
    {
        //Task <List<string>> GetUsers();

        public Guid InstanceId { get; }

        Task<List<string>> GetTask();
   }
}
