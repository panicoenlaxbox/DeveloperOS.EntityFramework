namespace Modeling
{
    public class User
    {
        public User()
        {
            Address = new Address();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public virtual Profile Profile { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
            private set { }
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Address)}: {Address}, {nameof(Profile)}: {Profile}, {nameof(FullName)}: {FullName}";
        }
    }
}