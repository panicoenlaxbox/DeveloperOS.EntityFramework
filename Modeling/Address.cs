namespace Modeling
{
    public class Address
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool HasValue => !string.IsNullOrWhiteSpace(City) ||
                                !string.IsNullOrWhiteSpace(PostalCode);

        public override string ToString()
        {
            return $"{nameof(City)}: {City}, {nameof(PostalCode)}: {PostalCode}, {nameof(HasValue)}: {HasValue}";
        }
    }
}