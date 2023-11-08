﻿using TodoList.DAL.Entities.Abstract;

namespace TodoList.DAL.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
