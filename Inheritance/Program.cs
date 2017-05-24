using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Inheritance.Models;
using Inheritance.Performance;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            //Performance();
            try
            {
                using (var context = new InheritanceContext())
                {
                    context.Database.Initialize(true);
                    context.Database.Log = Console.WriteLine;
                    IQueryable<Payment> payments = from p in context.Payments select p;
                    foreach (var payment in payments)
                    {
                        Console.WriteLine(payment.ToString());
                    }
                    IQueryable<CreditCardPayment> creditCards = from p in context.Payments.OfType<CreditCardPayment>()
                                                                select p;
                    foreach (var creditCard in creditCards)
                    {
                        Console.WriteLine(creditCard.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }

        private static void Performance()
        {
            SaveEntities();
            LoadEntities<Parent>();
            LoadEntities<Child1>();
            Console.ReadLine();
        }

        private static void SaveEntities()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<PerformanceContext>());
            using (var context = new PerformanceContext())
            {
                context.Database.Initialize(true);
                const int numberOf = 100;
                context.Entities.AddRange(CreateEntities<Child1>(numberOf));
                context.Entities.AddRange(CreateEntities<Child2>(numberOf));
                context.Entities.AddRange(CreateEntities<Child3>(numberOf));
                context.Entities.AddRange(CreateEntities<Child4>(numberOf));
                context.Entities.AddRange(CreateEntities<Child5>(numberOf));
                var stopWatch = Stopwatch.StartNew();
                context.SaveChanges();
                stopWatch.Stop();
                Console.WriteLine($"Guardar {stopWatch.Elapsed:g}");
            }
        }

        private static void LoadEntities<T>()
        {
            using (var context = new PerformanceContext())
            {
                context.Database.Log = Console.WriteLine;
                var stopWatch = Stopwatch.StartNew();
                (from p in context.Entities.OfType<T>() select p).ToList();
                Console.WriteLine($"{nameof(T)} query {stopWatch.Elapsed:g}");
            }
        }

        private static IEnumerable<T> CreateEntities<T>(int numberOf) where T : new()
        {
            var entities = new List<T>();
            for (var i = 0; i < numberOf; i++)
            {
                entities.Add(new T());
            }
            return entities;
        }
    }
}
