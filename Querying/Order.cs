using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Querying
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new Collection<OrderLine>();
    }
}