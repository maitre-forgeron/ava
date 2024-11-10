namespace Ava.Domain.Models.User
{
    public class TherapistCategory : Entity
    {
        public Guid TherapistId { get; private set; }
        public Therapist Therapist { get; private set; }

        public Guid CategoryId { get; private set; }
        public Category.Category Category { get; private set; }

        private TherapistCategory()
        {

        }
        public TherapistCategory(Guid therapistId, Guid categoryId) : this()
        {
            TherapistId = therapistId;
            CategoryId = categoryId;
        }
    }
}
