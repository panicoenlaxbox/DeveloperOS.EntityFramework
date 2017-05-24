using System.Collections.Generic;

namespace Querying.SqlToolbelt
{
    public interface ISqlParser
    {
        IEnumerable<string> Parse(string batch);
    }
}