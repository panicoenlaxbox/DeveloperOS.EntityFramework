namespace Models
{
    public class CreditCardPayment : Payment
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string Number { get; set; }

        public override string ToString() => $"{base.ToString()}, {nameof(Year)}: {Year}, {nameof(Month)}: {Month}, {nameof(Number)}: {Number}";
    }
}