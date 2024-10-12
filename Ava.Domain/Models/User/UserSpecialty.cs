namespace Ava.Domain.Models.User
{
    public class UserSpecialty : Entity
    {
        public Guid UserId { get; set; }
        public Guid SpecialtyId { get; set; }
    }
}