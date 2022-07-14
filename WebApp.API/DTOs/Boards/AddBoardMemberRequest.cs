using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Boards
{
    public class AddBoardMemberRequest
    {
        public AddBoardMemberRequest(int boardId, int userId)
        {
            BoardId = boardId;
            UserId = userId;
        }
        public int UserId { get; set; }
        public int BoardId { get; set; }
    }
}
