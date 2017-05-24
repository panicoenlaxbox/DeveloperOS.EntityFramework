namespace Modeling
{
    public class Profile
    {
        public int Id { get; set; }
        public string Twitter { get; set; }
        public byte[] Image { get; set; }
        public virtual User User { get; set; }
    }
}