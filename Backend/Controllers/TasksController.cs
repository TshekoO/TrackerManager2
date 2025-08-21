using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
 
    {
        private static readonly ConcurrentDictionary<int, TaskItem> _tasks = new();
        private static int _nextId = 1;
        private static readonly string[] ValidPriorities = { "High", "Medium", "Low" };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_tasks.Values);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title cannot be empty.");

            if (!ValidPriorities.Contains(dto.Priority))
                return BadRequest("Priority must be one of: High, Medium, Low.");

            var task = new TaskItem
            {
                Id = _nextId++,
                Title = dto.Title,
                Priority = dto.Priority,
                IsCompleted = false
            };
            _tasks[task.Id] = task;
            return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult MarkCompleted(int id)
        {
            if (!_tasks.TryGetValue(id, out var task))
                return NotFound();

            task.IsCompleted = true;
            return NoContent();
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Priority { get; set; } = "Low";
        public bool IsCompleted { get; set; }
    }

    public class TaskCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Priority { get; set; } = "Low";
    }
}