namespace Ava.Domain.Models.User;

public class Customer : UserProfile
{
    private Customer()
    {
    }

    public Customer(Guid userId, string userName, string email, string phone, string firstName, string lastName, string personalId) 
        : base(userId, userName, email, phone, firstName, lastName, personalId) 
    {
    }
}
