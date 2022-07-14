using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.DTOs.ListTasks;
using WebApp.API.DTOs.Users;

namespace WebApp.API.DTOs.Boards
{
    public class GetBoardResponse
    {
        public BoardResponse Board { get; set; }
        public List<AddUserResponse> Members { get; set; }
        public List<ListTaskReponseGetBoard> ListTasks { get; set; }
    }
}
