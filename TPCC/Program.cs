using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Models;

namespace TPCC
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new InheritanceContext("TPCC"))
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
            modelBuilder.Entity<Payment>()
                .Map<CreditCardPayment>(
                    configuration => configuration
                        .MapInheritedProperties()
                        .ToTable("CreditCardPayments"))
                .Map<PayPalPayment>(
                    configuration => configuration
                        .MapInheritedProperties()
                        .ToTable("PayPalPayments"));
            //modelBuilder.Entity<Payment>()
            //    .Property(p => p.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
