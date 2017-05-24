using System;
using System.Linq;

namespace Querying
{
    static class OrdersExtensions
    {
        public static IQueryable<Order> GetOrdersGreaterThanYear2000(this IQueryable<Order> orders)
        {
            return orders.Where(o => o.CreatedDate > new DateTime(2000, 1, 1));
        }
    }
}