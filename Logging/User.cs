using System.Collections.Generic;

namespace Logging
{
    public class User
    {
        public User()
        {

        }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
        public int Identifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{nameof(Identifier)}: {Identifier}, {nameof(Name)}: {Name}, {nameof(Email)}: {Email}";
        }
    }
}