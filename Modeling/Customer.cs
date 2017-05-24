namespace Modeling
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Preallocated fields
        public string C1 { get; set; }
        public string C2 { get; set; }
        public string C3 { get; set; }

        public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(C1)}: {C1}, {nameof(C2)}: {C2}, {nameof(C3)}: {C3}";
    }
}