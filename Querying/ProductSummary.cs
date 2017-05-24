namespace Querying
{
    public class ProductSummary
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}