using System;
using System.Data.Entity;
using System.Linq;
using Models;

namespace TPT
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new InheritanceContext("TPT"))
            {
                context.ModelCreating += OnModelCreating;
                context.Database.Initialize(true);
                context.Database.Log = Console.WriteLine;
                IQueryable<Payment> payments = from p in context.Payments select p;
                foreach (var payment in payments)
                {
                    Console.WriteLine(payment.ToString());
                }
                IQueryable<CreditCardPayment> creditCards =
                    from p in context.Payments.OfType<CreditCardPayment>() select p;
                foreach (var creditCard in creditCards)
                {
                    Console.WriteLine(creditCard.ToString());
                }
                Console.ReadLine();
            }
        }

        private static void OnModelCreating(object sender, EventArgs eventArgs)
        {
            var modelBuilder = (DbModelBuilder)sender;
            modelBuilder.Entity<CreditCardPayment>().ToTable("CreditCardPayments");
            modelBuilder.Entity<PayPalPayment>().ToTable("PayPalPayments");
        }
    }
}
