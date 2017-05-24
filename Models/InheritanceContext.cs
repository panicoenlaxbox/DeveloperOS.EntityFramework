using System;
using System.Data.Entity;

namespace Models
{
    public delegate void OnModelCreatingEventHandler(object sender, EventArgs e);

    public class InheritanceContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }

        public InheritanceContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new InheritanceDatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelCreating?.Invoke(modelBuilder, new EventArgs());
            base.OnModelCreating(modelBuilder);
        }

        public event OnModelCreatingEventHandler ModelCreating;
    }
}