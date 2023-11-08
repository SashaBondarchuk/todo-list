using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoList.BLL.Interfaces;
using TodoList.BLL.MappingProfiles;
using TodoList.BLL.Services;
using TodoList.Common.Auth.Abstractions;
using TodoList.DAL.Context;
using TodoList.WebAPI.Auth;

namespace TodoList.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTodoListContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("TodoListDbConnection");
            services.AddDbContext<TodoListDbContext>(options =>
                options.UseSqlServer(
                    connectionsString,
                    opt => opt.MigrationsAssembly(typeof(TodoListDbContext).Assembly.GetName().Name)));
        }

        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<UserIdStorage>();
            services.AddTransient<IUserIdSetter>(s => s.GetService<UserIdStorage>()!);
            services.AddTransient<IUserIdGetter>(s => s.GetService<UserIdStorage>()!);
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(TaskProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile)));
        }
    }
}
