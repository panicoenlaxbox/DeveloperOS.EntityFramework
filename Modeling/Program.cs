using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace Modeling
{
    class Program
    {
        static void Main(string[] args)
        {
            UseComplexType();
            UseTableSplitting();
            UseEntitySplitting();
            UseCalculatedField();
            Console.ReadLine();
        }

        private static void UseComplexType()
        {
            using (var context = new ModelingContext())
            {
                context.Users.Add(new User()
                {
                    FirstName = "Carmen",
                    LastName = "Olivas",
                    Address = new Address()
                    {
                        City = "Madrid",
                        PostalCode = "28054"
                    }
                });
                context.Users.Add(new User()
                {
                    FirstName = "Jimena",
                    LastName = "León"
                });
                context.SaveChanges();
            }
        }

        private static void UseTableSplitting()
        {
            using (var context = new ModelingContext())
            {
                var image = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "ProfileImage.jpg"));
                context.Users.Add(new User()
                {
                    FirstName = "Sergio",
                    Profile = new Profile()
                    {
                        Twitter = "panicoenlaxbox",
                        Image = image
                    }
                });
                context.SaveChanges();
            }
            using (var context = new ModelingContext())
            {
                context.Database.Log = Console.WriteLine;
                var user = context.Users.Single(u => u.FirstName == "Sergio");
                Console.WriteLine($"{user.Profile.Image.Length} bytes");
            }
        }

        private static void UseEntitySplitting()
        {
            using (var context = new ModelingContext())
            {
                var customer = new Customer()
                {
                    Name = "Sergio",
                    C1 = "panicoenlaxbox",
                    C2 = "http://panicoenlaxbox.blogspot.com",
                    C3 = "panicoenlaxbox@gmail.com"
                };
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        private static void UseCalculatedField()
        {
            using (var context = new ModelingContext())
            {
                context.Users.Add(new User()
                {
                    FirstName = "John",
                    LastName = "Doe"
                });
                context.SaveChanges();
                context.Database.Log = Console.WriteLine;
                var fullName = context.Users.Where(u => u.FullName == "John Doe").Select(u => u.FullName);
                Console.WriteLine(fullName);
            }
        }
    }
}