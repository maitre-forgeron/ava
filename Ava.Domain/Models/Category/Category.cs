namespace Ava.Domain.Models.Category
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        private List<Category> _subCategories = new List<Category>();

        public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (_subCategories.Contains(subCategory))
                throw new InvalidOperationException("This subcategory is already added.");

            _subCategories.Add(subCategory);
        }
    }
}
