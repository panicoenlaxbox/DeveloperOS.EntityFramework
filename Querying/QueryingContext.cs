using System.Data.Entity;

namespace Querying
{    
    public class QueryingContext : DbContext
    {
        public QueryingContext() : base("Querying") //base("Data Source=(local);Initial Catalog=Querying;Trusted_Connection=True")
        {
            Database.SetInitializer(new QueryingDatabaseInitializer());
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}