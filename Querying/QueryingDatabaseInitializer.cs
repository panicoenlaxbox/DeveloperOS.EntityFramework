using System.Data.Entity;
using System.Reflection;
using Querying.SqlToolbelt;

namespace Querying
{
    class QueryingDatabaseInitializer : DropCreateDatabaseAlways<QueryingContext>
    {
        protected override void Seed(QueryingContext context)
        {
            var executor = new SqlBatchExecutor(new SqlParser(), new SqlExecutor(context.Database.Connection));
            executor.ExecuteFromEmbeddedResource(Assembly.GetExecutingAssembly(), "Querying.Seed.sql");
            base.Seed(context);
        }
    }
}