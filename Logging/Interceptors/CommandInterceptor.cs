using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace Logging.Interceptors
{
    class CommandInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log("NonQueryExecuted", $"NonQueryExecuted {command.CommandText}");
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log("NonQueryExecuting", command.CommandText);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            Log("ReaderExecuted", command.CommandText);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            Log("ReaderExecuting", command.CommandText);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Log("ScalarExecuted", command.CommandText);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            var context = interceptionContext.DbContexts.FirstOrDefault() as LoggingContext;
            Log("ScalarExecuting", command.CommandText);
        }

        private static void Log(string command, string commandText)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"IDbCommandInterceptor.{command}, {commandText}");
            Console.ResetColor();
        }
    }
}
