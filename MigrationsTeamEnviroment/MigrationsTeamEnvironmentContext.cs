using System.Data.Entity;

namespace MigrationsTeamEnviroment
{
    class MigrationsTeamEnvironmentContext : DbContext
    {
        public MigrationsTeamEnvironmentContext() : base("MigrationsTeamEnvironment")
        {

        }
        public DbSet<User> Users { get; set; }
    }
}