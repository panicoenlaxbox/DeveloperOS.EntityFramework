using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using SqlSentence;

namespace Querying
{
    class Program
    {
        static void Main(string[] args)
        {
            LazyLoading();
            //EagerLoading();
            //ExplicitLoading();
            //DeferredExecution();
            //ImmediateExecution();
            //AsEnumerable();
            //AsQueryable();
            //ComplexQuery();
            //Dapper();
            //DynamicSql();
            Console.ReadLine();
        }

        private static void LazyLoading()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;
                var orders = context.Orders;
                var firstOrder = orders.First();
                var proxyType = firstOrder.GetType().Name;
                var type = ObjectContext.GetObjectType(firstOrder.GetType()).Name;
                Console.WriteLine($"{proxyType} / {type}");
                foreach (var order in orders)
                {
                    Console.WriteLine($"#{order.Id} has {order.OrderLines.Count} lines");
                }
            }
        }

        private static void EagerLoading()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;
                var orders = context.Orders
                    .Include(p => p.OrderLines)
                    .Include(p => p.OrderLines.Select(t => t.Product));
                foreach (var order in orders)
                {
                    Console.WriteLine($"#{order.Id} has {order.OrderLines.Count} lines");
                    Console.WriteLine($@"#{order.Id} has {order.OrderLines.Select(p => p.Product.Name).Distinct().Count()} different products");
                }
            }
        }

        private static void ExplicitLoading()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;
                foreach (var order in context.Orders)
                {
                    var orderEntry = context.Entry(order);
                    if (!orderEntry.Collection(o => o.OrderLines).IsLoaded)
                    {
                        Console.WriteLine($"Loading order lines for order #{order.Id}...");
                        orderEntry.Collection(o => o.OrderLines).Load();
                    }
                    foreach (var orderLine in order.OrderLines)
                    {
                        var orderLineEntry = context.Entry(orderLine);
                        if (!orderLineEntry.Reference(ol => ol.Product).IsLoaded)
                        {
                            Console.WriteLine($"Loading product for order line #{orderLine.Id}...");
                            orderLineEntry.Reference(ol => ol.Product).Load();
                            Console.WriteLine($"Product {orderLine.Product.Name} loaded");
                        }
                        Console.WriteLine($"Order line #{orderLine.Id} of order {order.Id} has the product {orderLine.Product.Name}");
                    }
                }
                context.SaveChanges();
            }
        }

        private static void DeferredExecution()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;
                var orders = context.Orders;
                foreach (var order in orders)
                {
                    Console.WriteLine($"#{order.Id}");
                }
                CreateOrder();
                foreach (var order in orders)
                {
                    Console.WriteLine($"#{order.Id}");
                }
            }
        }

        private static void ImmediateExecution()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;
                var orders = context.Orders.ToList();
                foreach (var order in orders)
                {
                    Console.WriteLine($"#{order.Id}");
                }
                CreateOrder();
                foreach (var order in orders)
                {
                    Console.WriteLine($"#{order.Id}");
                }
            }
        }

        private static void AsEnumerable()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;

                var q = context.Products
                    .Select(p => p.Name.ToUpper())
                    .AsEnumerable()
                    .Select(p => Regex.Replace(p, "[AEIOU]", "#"));
                foreach (var item in q)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void AsQueryable()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;
                var q = context.Products
                    .Select(p => p.Name.ToUpper())
                    .ToLower();
                foreach (var item in q)
                {
                    Console.WriteLine(item);
                }
                var q2 = context.Products
                    .Select(p => p.Name.ToUpper())
                    .AsEnumerable()
                    .Select(p => Regex.Replace(p, "[AEIOU]", "#"))
                    .AsQueryable()
                    .ToLower();
                foreach (var item in q2)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void ComplexQuery()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;

                var q = from o in context.Orders.GetOrdersGreaterThanYear2000()
                        .Where(IsOrderGreaterThanYear2000())
                        from l in o.OrderLines
                        where o.Id > 1
                        select new
                        {
                            o,
                            l,
                            p = l.Product
                        };
                const bool filter = true;
                if (filter)
                {
                    q = from r in q
                        where r.l.Units > 0
                        select r;
                }
                var q2 = from r in q
                         group r by r.p.Name
                    into cont
                         select new
                         {
                             Product = cont.Key,
                             Units = cont.Sum(p => p.l.Units)
                         };
                foreach (var item in q2)
                {
                    Console.WriteLine($"{item.Product}, {item.Units}");
                }
            }
        }

        private static void Dapper()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;

                var data = context.Database.SqlQuery<OrderLineSummary>(@"
                    SELECT Id, Units
                    FROM OrderLines
                    WHERE OrderId = {0}", 1);
                foreach (var item in data)
                {
                    Console.WriteLine(item);
                }

                var data2 = context.Database.Connection.Query<OrderLineSummary, ProductSummary, OrderLineSummary>(
                    sql: @"SELECT OrderLines.Id, Units, Products.Id AS ProductId, Name
                    FROM OrderLines INNER JOIN Products ON OrderLines.ProductId = Products.Id
                    WHERE OrderId = @OrderId",
                    map: (ol, p) =>
                    {
                        ol.Product = new ProductSummary
                        {
                            Name = p.Name
                        };
                        return ol;
                    },
                    param: new { OrderId = 1 },
                    splitOn: "ProductId");
                foreach (var item in data2)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void DynamicSql()
        {
            using (var context = new QueryingContext())
            {
                context.Database.Initialize(true);
                context.Database.Log = Log;

                var sql = new SqlSentenceBuilder();
                dynamic parameters = new ExpandoObject();
                sql.AddSelect("SUM(Units) AS Units, Name");
                sql.AddFrom("OrderLines INNER JOIN Products ON OrderLines.ProductId = Products.Id");
                sql.AddGroupBy("Name");
                const bool filter = true;
                if (filter)
                {
                    sql.AddWhere("OrderId = @OrderId", "myFilter");
                    parameters.OrderId = 1;
                    //sql.RemoveWhere("myFilter");
                    //((IDictionary<string, object>)parameters).Remove("OrderId");
                }
                sql.AddOrderBy("Name");
                sql.EnablePaginationWithCte(1, 10);
                Console.WriteLine(sql.Build());
                var data = context.Database.Connection.Query<ComplexSqlType>(sql.Build(), (ExpandoObject)parameters);
                foreach (var item in data)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void Log(string value)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        private static void CreateOrder()
        {
            using (var context = new QueryingContext())
            {
                context.Orders.Add(new Order());
                context.SaveChanges();
            }
        }

        private static Expression<Func<Order, bool>> IsOrderGreaterThanYear2000()
        {
            return o => o.CreatedDate > new DateTime(2000, 1, 1);
        }
    }
}
