﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weather_jwt.Models
{
    public class ApiUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Role { get; set; }

    }
}
