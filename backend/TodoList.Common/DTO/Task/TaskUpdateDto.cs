﻿namespace TodoList.Common.DTO.Task
{
    public class TaskUpdateDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}
