using Bogus;
using Microsoft.EntityFrameworkCore;
using TodoList.Common.Auth.Helpers;
using TodoList.DAL.Entities;
using Task = TodoList.DAL.Entities.Task;

namespace TodoList.DAL.Context.Extensions
{
    public static class ModelBuilderExtensions
    {
        private const int USERS_ENTITY_COUNT = 7;
        private const int TASKS_ENTITY_COUNT = 20;
        private static readonly DateTime _usedDateTime = new(2023, 11, 07);

        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne(t => t.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            Randomizer.Seed = new Random(1234567);

            var users = GenerateRandomUsers();
            var tasks = GenerateRandomTasks(users);

            modelBuilder.Entity<Task>().HasData(tasks);
            modelBuilder.Entity<User>().HasData(users);
        }

        private static ICollection<User> GenerateRandomUsers()
        {
            Faker.GlobalUniqueIndex = 0;

            return new Faker<User>()
                .RuleFor(u => u.Id, f => f.IndexGlobal)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.PasswordHash, f => PasswordHasher.HashPassword(f.Internet.Password()))
                .Generate(USERS_ENTITY_COUNT);
        }

        private static ICollection<Task> GenerateRandomTasks(ICollection<User> users)
        {
            Faker.GlobalUniqueIndex = 0;

            return new Faker<Task>()
                .RuleFor(t => t.Id, f => f.IndexGlobal)
                .RuleFor(t => t.IsCompleted, f => f.Random.Bool())
                .RuleFor(t => t.UserId, f => f.PickRandom(users).Id)
                .RuleFor(t => t.Title, f => f.Lorem.Sentence())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.CreatedAt, f => _usedDateTime)
                .Generate(TASKS_ENTITY_COUNT);
        }
    }
}
