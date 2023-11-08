using Microsoft.AspNetCore.Mvc;
using TodoList.BLL.Interfaces;
using TodoList.Common.Auth.Attributes;
using TodoList.Common.DTO.Task;

namespace TodoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("UserTasks"), BasicAuthorization]
        public async Task<IActionResult> GetTasksAsync()
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpPost("Create"), BasicAuthorization]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskCreateDto taskCreateDto)
        {
            var createdTask = await _taskService.CreateNewTaskAsync(taskCreateDto);
            return Ok(createdTask);
        }

        [HttpPut("edit"), BasicAuthorization]
        public async Task<IActionResult> UpdateTaskAsync([FromBody] TaskUpdateDto taskUpdateDto)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(taskUpdateDto);
            return Ok(updatedTask);
        }

        [HttpDelete, BasicAuthorization]
        public async Task<IActionResult> DeleteTaskAsync([FromQuery] int taskId)
        {
            await _taskService.DeleteAsync(taskId);
            return NoContent();
        }
    }
}
