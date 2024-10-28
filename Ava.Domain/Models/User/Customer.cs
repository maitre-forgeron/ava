namespace Ava.Domain.Models.User;

public class Customer : UserProfile
{
    private Customer()
    {
    }

    public Customer(Guid userId, string firstName, string lastName, string personalId) 
        : base(userId, firstName, lastName, personalId) 
    {
    }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
