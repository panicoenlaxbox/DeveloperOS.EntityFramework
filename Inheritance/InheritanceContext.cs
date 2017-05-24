using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Inheritance.Models;

namespace Inheritance
{
    class InheritanceContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }

        public InheritanceContext()
        {
            Database.SetInitializer(new InheritanceDatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //ConfigureTablePerHierarchy(modelBuilder);
            //TablePerType(modelBuilder);
            //TablePerConcreteClass(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void TablePerConcreteClass(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Map<CreditCardPayment>(
                    configuration => configuration
                        .MapInheritedProperties()
                        .ToTable("CreditCardPayments"))
                .Map<PayPalPayment>(
                    configuration => configuration
                        .MapInheritedProperties()
                        .ToTable("PayPalPayments"));
            FixTablePerConcreteClass(modelBuilder);
        }

        private static void FixTablePerConcreteClass(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }

        private static void TablePerType(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCardPayment>().ToTable("CreditCardPayments");
            modelBuilder.Entity<PayPalPayment>().ToTable("PayPalPayments");
        }

        private static void ConfigureTablePerHierarchy(DbModelBuilder modelBuilder)
        {
            const string discriminator = "MyDiscriminator";
            modelBuilder.Entity<Payment>()
                .Map<CreditCardPayment>(configuration => configuration.Requires(discriminator).HasValue(1))
                .Map<PayPalPayment>(configuration => configuration.Requires(discriminator).HasValue(2));
        }
    }
}