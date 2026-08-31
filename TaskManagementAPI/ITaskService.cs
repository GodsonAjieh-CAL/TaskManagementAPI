namespace TaskManagementAPI
{
    public interface ITaskService
    {
        //Task <List<string>> GetUsers();

        Task<List<string>> GetTask();
   }
}
