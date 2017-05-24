using System;
using System.Linq;

namespace Logging
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LoggingContext())
            {
                var user = new User("Sergio", "panicoenlaxbox@gmail.com");
                context.Users.Add(user);
                context.SaveChanges();
                user.Email = "johndoe@gmail.com";
                context.SaveChanges();
            }
            using (var context = new LoggingContext())
            {
                var user = context.Users.Single(u => u.Name == "Sergio");
                Console.WriteLine(user);
                context.Users.Remove(user);
                context.SaveChanges();
            }
            Console.ReadLine();
        }
    }
}
