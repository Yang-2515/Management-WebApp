using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.API.DTOs.Boards;
using WebApp.API.DTOs.ListTasks;
using WebApp.Domain.Boards;
using WebApp.Service;

namespace WebApp.API.Controllers
{
    [Route("api/board")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly BoardService _boardService;
        private readonly IMediator _mediator;
        public BoardController(BoardService boardService,
            IMediator mediator)
        {
            _boardService = boardService;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync([FromBody] AddBoard board)
        {

            await _boardService.AddBoardAsync(board, Convert.ToInt32(GetUserIdFromToken()));
            return Ok();
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
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(GetBoardResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBoardAsync([FromRoute] int id)
        {
            var board = await _boardService.GetBoardAsync(id, GetUserIdFromToken());
            if (board.Board == null) return NotFound();
            return Ok(board);
        }
        [HttpPut]
        [ProducesResponseType(typeof(GetBoardResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateBoardRequest updateBoardRequest)
        {
            await _boardService.UpdateBoard(updateBoardRequest);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _boardService.DeleteBoard(id);
            return Ok();
        }
        [HttpPost("boardMember")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBoardMember([FromBody] AddBoardMemberRequest boardMember)
        {
            await _boardService.AddBoardMember(boardMember);
            return Ok();
        }

        [HttpDelete("boardMember")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBoardMember([FromQuery] int userId, [FromQuery] int boardId)
        {
            await _boardService.DeleteByUserIdAndBoardTd(userId, boardId);
            return Ok();
        }
        [HttpPost("listTask")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddListTaskAsync([FromBody] AddListTaskRequest listTaskRequest)
        {

            await _boardService.AddListTaskAsync(listTaskRequest);
            return Ok();
        }
        [HttpGet("listTask/{id:int}")]
        [ProducesResponseType(typeof(GetListTaskResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListTaskAsync([FromRoute] int id)
        {
            return Ok(await _boardService.GetListTaskAsync(id));
        }
        [HttpPut("listTask")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateListTaskAsync([FromBody] UpdateListTaskRequest updateListTaskRequest)
        {
            await _boardService.UpdateListTaskAsync(updateListTaskRequest);
            return Ok();
        }
        [HttpDelete("listTask/{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteListTaskAsync([FromRoute] int id)
        {
            await _boardService.DeleteListTaskAsync(id);
            return Ok();
        }
    }
}
