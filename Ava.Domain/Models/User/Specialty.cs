namespace Ava.Domain.Models.User
{
    public class Specialty : Entity
    {
        public Specialty(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
