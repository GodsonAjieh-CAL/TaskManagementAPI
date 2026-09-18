using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlTypes;

/*Controllers ask for dependencies, they dont ccreate dependencies */

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
        public async Task<IActionResult> GetTask()
        {
            //This gives an ITaskService
            var instance = HttpContext.RequestServices.GetService<ITaskService>();

            var result = new
            {
                //from constructor injection
                FirstInstanceId = _taskService.InstanceId,
                //from asking the container manuallyS
                SecondInstanceId = instance.InstanceId,

                Tasks = await _taskService.GetTask()
            };


            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskById(id);

            if (task is null)
            {
                return NotFound();
            }
            return Ok(task);
        }
    }
}
