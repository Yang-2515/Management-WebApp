using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Histories
{
    public class AddHistoryRequest
    {
        public string Action { get; set; }
        public string Message { get; set; }
        public string RefType { get; set; }
        public int RefId { get; set; }
    }
}
