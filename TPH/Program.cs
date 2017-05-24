using System;
using System.Data.Entity;
using System.Linq;
using Models;

namespace TPH
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new InheritanceContext("TPH"))
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
            const string discriminator = "MyDiscriminator";
            var modelBuilder = (DbModelBuilder) sender;
            modelBuilder.Entity<Payment>()
                .Map<CreditCardPayment>(configuration => configuration.Requires(discriminator).HasValue(1))
                .Map<PayPalPayment>(configuration => configuration.Requires(discriminator).HasValue(2));
        }
    }
}