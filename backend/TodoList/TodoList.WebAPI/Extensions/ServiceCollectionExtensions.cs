using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Context;

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
    }
}
