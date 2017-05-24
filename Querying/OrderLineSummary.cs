namespace Querying
{
    public class OrderLineSummary
    {
        public int Id { get; set; }
        public int Units { get; set; }
        public ProductSummary Product { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Units)}: {Units}, {nameof(Product)}: {Product}";
        }
    }
}