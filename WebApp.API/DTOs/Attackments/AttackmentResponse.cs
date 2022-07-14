using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Attackments
{
    public class AttackmentResponse
    {
        public int Id { get; set; }
        public string StorageURL { get; set; }
        public string FileName { get; set; }
    }
}
