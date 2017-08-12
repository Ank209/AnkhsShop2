using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int AisleNumber { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public int ItemTypeId { get; set; }
        public virtual ItemType ItemType { get; set; }

        public virtual List<Request> Requests { get; set; }
    }
}