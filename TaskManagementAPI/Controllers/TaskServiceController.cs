using Microsoft.AspNetCore.Mvc;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskServiceController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskServiceController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpGet(Name = "GetAll")]
        public Task<List<string>> GetTask()
        {
            return _taskService.GetTask();
        }
    }
}
