using System.Data.Entity;

namespace Logging.Interceptors
{
    public class EFConfiguration : DbConfiguration
    {
        public EFConfiguration()
        {
            AddInterceptor(new CommandInterceptor());
            AddInterceptor(new CommandTreeInterceptor());
        }
    }
}