using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string MainTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public int SequelNum { get; set; }
        public bool BluRay { get; set; }
        public string UPC { get; set; }
    }
}