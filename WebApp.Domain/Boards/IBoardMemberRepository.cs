using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Interfaces;

namespace WebApp.Domain.Boards
{
    public interface IBoardMemberRepository: IAsyncRepository<BoardMember>
    {
    }
}
