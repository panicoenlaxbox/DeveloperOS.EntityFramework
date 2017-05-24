using System.Data.Entity;

namespace Models
{
    internal class InheritanceDatabaseInitializer : DropCreateDatabaseAlways<InheritanceContext>
    {
        protected override void Seed(InheritanceContext inheritanceContext)
        {
            var creditCardPayment = new CreditCardPayment()
            {
                //Id = 1, // TPCC
                Name = "Sergio",
                Year = 2017,
                Month = 5,
                Number = "12345"
            };
            inheritanceContext.Orders.Add(new Order()
            {
                Payment = creditCardPayment
            });
            var payPalPayment = new PayPalPayment()
            {
                //Id = 2, // TPCC
                Name = "Sergio",
                Email = "panicoenlaxbox@gmail.com"
            };
            inheritanceContext.Orders.Add(new Order()
            {
                Payment = payPalPayment
            });
            base.Seed(inheritanceContext);
        }
    }
}