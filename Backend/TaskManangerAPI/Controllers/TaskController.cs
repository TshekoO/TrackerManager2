using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using TaskManangerAPI.Models;
using TaskManangerAPI.Services; 

namespace TaskManangerAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]

    public class TaskController : ControllerBase
    {
        private readonly TaskServices _service;
        private readonly string[] _validPriorities = { "High", "Medium", "Low" };
        public TaskController(TaskServices service)
        {
            _service = service;
        }
        // GET /tasks - Retrieve all tasks
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
        //POST /tasks - Create a new task
        [HttpPost]
        public IActionResult Create([FromBody] TaskItem item)
        {
            var (task, error) = _service.Add(item);
            if(error != null)
            {
                return BadRequest(error); // Return 400 Bad Request with error message
            }
            return CreatedAtAction(nameof(GetAll), new { id = task!.Id }, task); // Return 201 Created with the new task
        }

        //PUT - Mark task as completed 
        [HttpPut("{id}")]
        public IActionResult MarkCompleted(int id)
        {

            if (_service.MarkCompleted(id))
                return NoContent();
            return NotFound(); // It returns 204 No content on success
        }
    }
}
