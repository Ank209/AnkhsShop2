using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreLoc { get; set; }

        public virtual List<Item> Items { get; set; }
    }
}