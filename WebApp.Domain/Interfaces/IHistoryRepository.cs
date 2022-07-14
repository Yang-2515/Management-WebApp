using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Interfaces;

namespace WebApp.Domain.Histories
{
    public interface IHistoryRepository: IAsyncRepository<History>
    {
    }
}
