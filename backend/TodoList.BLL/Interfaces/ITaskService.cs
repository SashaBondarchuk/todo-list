using TodoList.Common.DTO.Task;

namespace TodoList.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetTasksAsync();

        Task<TaskDto> CreateNewTaskAsync(TaskCreateDto taskCreateDto);

        Task DeleteAsync(int taskId);

        Task<TaskDto> UpdateTaskAsync(TaskUpdateDto taskUpdateDto);
    }
}
