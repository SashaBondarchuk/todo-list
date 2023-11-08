using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Context.Extensions;
using TodoList.DAL.Entities;

namespace TodoList.DAL.Context
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options) { }

        public DbSet<Entities.Task> Tasks { get; private set; }
        public DbSet<User> Users { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();

            modelBuilder.Seed();
        }
    }
}
