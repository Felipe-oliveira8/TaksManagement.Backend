using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TaksManagement.Backend.Models;
using TaksManagement.Backend.Services;

namespace TaksManagement.Backend.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        public TaskAppService TaskAppService { get; set; }
        public ActivityAppService ActivityAppService { get; set; }

        public TaskController(TaskAppService taskAppService, ActivityAppService activityAppService)
        {
            TaskAppService = taskAppService;
            ActivityAppService = activityAppService;
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            TaskAppService.Create(task, User.Identity.Name);
            return Created(nameof(task), task);
        }

        [HttpPost("activity")]
        public IActionResult AddActivity([FromBody] ActivityModel activity)
        {
            try
            {
                var task = TaskAppService.GetTaskActivity(activity.TaskId);
                ActivityAppService.Create(activity, task, User.Identity.Name);
                return Created(nameof(AddActivity), activity);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Apenas o Gerador ou Responsavel podem cadastrar atividades!");
            }

        }

        [HttpGet("list")]
        public IActionResult GetTasks()
        {
            return Ok(TaskAppService.GetTasks());
        }

        [HttpGet("{taskId}")]
        public IActionResult GetTask([FromRoute] string taskId)
        {
            return Ok(TaskAppService.GetTask(taskId));
        }

        [HttpGet("activity-list/{taskId}")]
        public IActionResult GetActivities([FromRoute] string taskId)
        {
            return Ok(ActivityAppService.GetActivities(taskId));
        }

        [HttpPut("{taskId}")]
        public IActionResult Update([FromRoute] string taskId, [FromBody] TaskModel updateTask)
        {
            TaskAppService.Update(updateTask, User.Identity.Name);
            return NoContent();
        }
    }
}
