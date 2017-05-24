using System.Collections.Generic;

namespace Querying.SqlToolbelt
{
    public interface ISqlExecutor
    {
        void Execute(string sql);
        void Execute(IEnumerable<string> batch);
    }
}