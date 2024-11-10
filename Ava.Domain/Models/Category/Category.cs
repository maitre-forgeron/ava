using Ava.Domain.Models.User;

namespace Ava.Domain.Models.Category;

public class Category : Entity
{
    public string Name { get; private set; }

    private List<Category> _subCategories;
    public IReadOnlyCollection<Category> SubCategories => _subCategories?.AsReadOnly();

    private List<TherapistCategory> _therapistCategories;
    public IReadOnlyCollection<TherapistCategory> TherapistCategories => _therapistCategories.AsReadOnly();

    private Category()
    {
    }

    public Category(Guid id, string name) : base(id)
    {
        Id = id;
        Name = name;
    }

    public void AddSubCategory(Category subCategory)
    {
        if (subCategory == null)
        {
            throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");
        }

        if (_subCategories is null)
        {
            _subCategories = new List<Category>();
        }

        if (_subCategories.Any(category => category.Id == subCategory.Id))
        {
            throw new InvalidOperationException("This subcategory is already added.");
        }

        _subCategories.Add(subCategory);
    }
}
