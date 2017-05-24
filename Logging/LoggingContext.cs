using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Logging.Interceptors;

namespace Logging
{
    [DbConfigurationType(typeof(EFConfiguration))]
    public class LoggingContext : DbContext
    {
        public LoggingContext() : base("Logging")
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Properties()
                .Where(p => p.Name == "Identifier")
                .Configure(p => p.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsKey());
            modelBuilder
                .Properties()
                .Where(p => p.Name == "Name")
                .Configure(p => p.HasMaxLength(50));
            base.OnModelCreating(modelBuilder);
        }
    }
}