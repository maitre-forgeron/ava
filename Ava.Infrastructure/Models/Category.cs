namespace Ava.Infrastructure.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Category> SubCategories { get; set; }
    }
}
