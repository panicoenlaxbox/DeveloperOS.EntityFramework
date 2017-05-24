namespace Querying
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Units { get; set; }
    }
}