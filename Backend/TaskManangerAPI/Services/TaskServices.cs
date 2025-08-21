using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using TaskManangerAPI.Models;

namespace TaskManangerAPI.Services
{
    public class TaskServices
    {
        private readonly List<TaskItem> _tasks = new(); // In-memory list to store tasks
        private int _nextId = 1; // To generate unique IDs for tasks
        private static readonly HashSet<string> ValidPriorities = new() { "High", "Medium", "Low" }; // Valid priorities
        //Return all tasks

        public IEnumerable<TaskItem> GetAll() => _tasks;

       
        public (TaskItem? task, string? error) Add(TaskItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
                return (null, "Title cannot be empty.");
            if (!ValidPriorities.Contains(item.Priority))
                return (null, "Priority must be High, Medium, or Low.");

            item.Id = _nextId++;
            item.IsCompleted = false;
            _tasks.Add(item);
            return (item, null);
        }

        public bool MarkCompleted(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;
            task.IsCompleted = true;
            return true;
        }
       
    }
}
