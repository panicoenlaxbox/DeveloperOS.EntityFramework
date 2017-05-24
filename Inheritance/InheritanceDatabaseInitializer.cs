using System.Data.Entity;
using Inheritance.Models;

namespace Inheritance
{
    class InheritanceDatabaseInitializer : DropCreateDatabaseAlways<InheritanceContext>
    //class InheritanceDatabaseInitializer : CreateDatabaseIfNotExists<InheritanceContext>
    {
        protected override void Seed(InheritanceContext context)
        {
            var creditCardPayment = new CreditCardPayment()
            {
                Id = 1,
                Name = "Sergio", Year = 2017, Month = 5, Number = "12345"
            };
            context.Orders.Add(new Order()
            {
                Payment = creditCardPayment
            });
            var payPalPayment = new PayPalPayment()
            {
                Id = 2,
                Name = "Sergio", Email = "panicoenlaxbox@gmail.com"
            };
            context.Orders.Add(new Order()
            {
                Payment = payPalPayment
            });
            base.Seed(context);
        }
    }
}