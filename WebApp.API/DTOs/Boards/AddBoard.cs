﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Boards
{
    public class AddBoard
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
