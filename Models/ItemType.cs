using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class ItemType
    {
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }

        public virtual List<Item> Items { get; set; }
    }
}