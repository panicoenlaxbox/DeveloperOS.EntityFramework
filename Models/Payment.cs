namespace Models
{
    public abstract class Payment : EntityBase
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Name)}: {Name}";
        }
    }
}