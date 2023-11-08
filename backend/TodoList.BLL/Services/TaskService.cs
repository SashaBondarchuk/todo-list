using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoList.BLL.Interfaces;
using TodoList.BLL.Services.Abstract;
using TodoList.Common.Auth.Abstractions;
using TodoList.Common.DTO.Task;
using TodoList.DAL.Context;

namespace TodoList.BLL.Services
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly IUserIdGetter _userIdGetter;

        public TaskService(TodoListDbContext context, IMapper mapper, IUserIdGetter userIdGetter) : base(context, mapper)
        {
            _userIdGetter = userIdGetter;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync()
        {
            var uid = _userIdGetter.CurrentUserId;
            var tasks = await _context.Tasks.Where(t => t.UserId == uid).ToListAsync();
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public async Task<TaskDto> CreateNewTaskAsync(TaskCreateDto taskCreateDto)
        {
            var uid = _userIdGetter.CurrentUserId;
            var newTask = _mapper.Map<DAL.Entities.Task>(taskCreateDto);

            newTask.UserId = uid;
            newTask.IsCompleted = false;
            newTask.CreatedAt = DateTime.Now;

            _context.Tasks.Add(newTask);

            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDto>(newTask);
        }

        public async Task<TaskDto> UpdateTaskAsync(TaskUpdateDto taskUpdateDto)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskUpdateDto.Id)
                       ?? throw new InvalidOperationException("Task was not found");

            var uid = _userIdGetter.CurrentUserId;
            if (task.UserId != uid)
            {
                throw new InvalidOperationException("You are not allowed to delete this task");
            }

            task.Id = taskUpdateDto.Id;
            task.IsCompleted = taskUpdateDto.IsCompleted;
            task.Title = taskUpdateDto.Title;
            task.Description = taskUpdateDto.Description;

            _context.Update(task);

            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDto>(task);
        }

        public async Task DeleteAsync(int taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId)
                ?? throw new InvalidOperationException("Task was not found");

            var uid = _userIdGetter.CurrentUserId;
            if (task.UserId != uid)
            {
                throw new InvalidOperationException("You are not allowed to delete this task");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
