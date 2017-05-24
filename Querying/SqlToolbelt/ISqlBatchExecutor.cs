using System.Reflection;

namespace Querying.SqlToolbelt
{
    public interface ISqlBatchExecutor
    {
        void ExecuteFromEmbeddedResource(Assembly assembly, string resourceName);
        void ExecuteFromFile(string path);
    }
}