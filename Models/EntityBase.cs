namespace Models
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}";
        }
    }
}