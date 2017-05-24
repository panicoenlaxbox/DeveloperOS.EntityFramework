namespace Models
{
    public class PayPalPayment : Payment
    {
        public string Email { get; set; }

        public override string ToString() => $"{base.ToString()}, {nameof(Email)}: {Email}";
    }
}