using AutoMapper;
using TodoList.Common.DTO.Task;
using Task = TodoList.DAL.Entities.Task;

namespace TodoList.BLL.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDto>();

            CreateMap<TaskCreateDto, Task>();
        }
    }
}
