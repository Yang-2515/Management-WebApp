using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.DTOs.Boards;

namespace WebApp.API.DTOs.Users
{
    public class GetUserReponse
    {
        public AddUserResponse User { get; set; }
        public List<BoardGetUserReponse> Boards { get; set; }
    }
}
