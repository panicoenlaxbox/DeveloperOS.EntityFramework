using System.Data.Entity;

namespace Modeling
{
    class ModelingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public ModelingContext() : base("Modeling")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ModelingContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ComplexType(modelBuilder);
            TableSplitting(modelBuilder);
            EntitySplitting(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void ComplexType(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types<User>()
                .Configure(configuration =>
                {
                    configuration.Property(p => p.Address.City).HasColumnName("City");
                    configuration.Property(p => p.Address.PostalCode).HasColumnName("PostalCode");
                });
            modelBuilder.ComplexType<Address>();
        }

        private static void TableSplitting(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(e => e.Profile)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(true);
        }

        private static void EntitySplitting(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Map(map =>
                {
                    map.Properties(p => new
                    {
                        p.Id,
                        p.Name
                    });
                    map.ToTable("Customers");
                })
                .Map(map =>
                {
                    map.Properties(p => new
                    {
                        p.C1,
                        p.C2,
                        p.C3
                    });
                    map.ToTable("CustomerFields");
                });
        }
    }
}