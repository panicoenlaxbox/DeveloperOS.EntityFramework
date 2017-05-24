namespace Models
{
    public class Order : EntityBase
    {
        public int PaymentId { get; set; }

        // Referencia polimórfica
        public virtual Payment Payment { get; set; }

        public override string ToString() => $"{nameof(PaymentId)}: {PaymentId}, {nameof(Payment)}: {Payment}";
    }
}