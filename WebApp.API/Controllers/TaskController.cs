using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.API.DTOs.Attackments;
using WebApp.API.DTOs.Histories;
using WebApp.API.DTOs.Tasks;
using WebApp.API.DTOs.ToDos;
using WebApp.API.Services.Histories;
using WebApp.API.Services.Tasks;

namespace WebApp.API.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        public TaskController(TaskService taskService,
            IMediator mediator)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync([FromBody] AddTaskRequest taskRequest)
        {
            await _taskService.AddTaskAsync(taskRequest);
            return Ok();
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetTaskResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTaskAsync([FromRoute] int id)
        {
            return Ok(await _taskService.GetTaskAsync(id));
        }
        protected long GetUserIdFromToken()
        {
            long UserId = 0;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string strUserId = identity.FindFirst("UserId").Value;
                        long.TryParse(strUserId, out UserId);

                    }
                }
                return UserId;
            }
            catch
            {
                return UserId;
            }
        }
        protected string GetMethod()
        {
            return HttpContext.Request.Method;
        }
        [HttpPut]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTaskRequest updateTaskRequest)
        {
            await _taskService.UpdateTaskAsync(updateTaskRequest);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok();
        }
        [HttpPost("taskMember")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddTaskMember([FromBody] AddTaskMemberRequest taskMember)
        {
            await _taskService.AddTaskMemberAsync(taskMember);
            return Ok();
        }

        [HttpDelete("taskMember")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteTaskMember([FromQuery] int userId, [FromQuery] int taskId)
        {
            await _taskService.DeleteTaskMemberByUserIdAndTaskTdAsync(userId, taskId);
            return Ok();
        }
        [HttpPatch("listTaskId")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateListTaskIdAsync([FromQuery] int taskId,[FromQuery] int listTaskId)
        {
            await _taskService.UpdateListTaskIdAsync(taskId, listTaskId);
            return Ok();
        }
        [HttpPatch("assignee")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAssigneeAsync([FromQuery] int taskId, [FromQuery] int assigneeId)
        {
            await _taskService.UpdateAssigneeAsync(taskId, assigneeId);
            return Ok();
        }
        [HttpPatch("priority")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePriorityAsync([FromQuery] int taskId, [FromQuery] string priority)
        {
            await _taskService.UpdatePriorityAsync(taskId, priority);
            return Ok();
        }
        [HttpPost("attackment")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAttackment([FromBody] AddAttackmentRequest attackment)
        {
            await _taskService.AddAttackmentAsync(attackment);
            return Ok();
        }

        [HttpDelete("attackment")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAttackment([FromQuery] int id)
        {
            await _taskService.DeleteAttackmentAsync(id);
            return Ok();
        }

        [HttpPost("taskLabel")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddLabelAsync([FromBody] AddTaskLabelRequest taskLabel)
        {
            await _taskService.AddTaskLabelAsync(taskLabel);
            return Ok();
        }

        [HttpDelete("taskLabel")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteLabelByLabelIdAndTaskIdAsync([FromQuery] int labelId, [FromQuery] int taskId)
        {
            await _taskService.DeleteLabelByLabelIdAndTaskIdAsync(labelId, taskId);
            return Ok();
        }
        [HttpPost("toDo")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddToDoAsync([FromBody] AddToDoRequest toDoRequest)
        {
            await _taskService.AddToDoAsync(toDoRequest);
            return Ok();
        }
        [HttpDelete("toDo/{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteToDoAsync([FromRoute] int id)
        {
            await _taskService.DeleteToDoAsync(id);
            return Ok();
        }
        [HttpPatch("toDo/name")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateNameAsync([FromQuery] int toDoId, [FromQuery] string name)
        {
            await _taskService.UpdateNameToDoAsync(toDoId, name);
            return Ok();
        }
        [HttpPatch("toDo/isComplete")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateIsCompleteAsync([FromQuery] int toDoId, [FromQuery] bool isComplete)
        {
            await _taskService.UpdateIsCompleteToDoAsync(toDoId, isComplete);
            return Ok();
        }
    }
}
