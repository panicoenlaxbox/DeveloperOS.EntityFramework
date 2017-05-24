using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure.Interception;

namespace Logging.Interceptors
{
    class CommandTreeInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            Log(interceptionContext.OriginalResult.GetType().Name);
            if (interceptionContext.OriginalResult is DbQueryCommandTree)
            {
            }
            else if (interceptionContext.OriginalResult is DbFunctionCommandTree)
            {

            }
            else if (interceptionContext.OriginalResult is DbInsertCommandTree)
            {

            }
            else if (interceptionContext.OriginalResult is DbModificationCommandTree)
            {

            }
            else if (interceptionContext.OriginalResult is DbQueryCommandTree)
            {

            }
            else if (interceptionContext.OriginalResult is DbUpdateCommandTree)
            {

            }
        }

        private static void Log(string value)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"IDbCommandTreeInterceptor.TreeCreated, {value}");
            Console.ResetColor();
        }
    }
}