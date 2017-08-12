using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public float Amount { get; set; }
        public int Frequency { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Bought { get; set; }
        public DateTime DateBought { get; set; }

        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}