﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }

        public virtual List<Request> Requests { get; set; }
    }
}