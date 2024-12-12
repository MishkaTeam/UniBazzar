using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;

namespace Domain.Aggregates.Categories;

public class Category : Entity
{

    public Category()
    {
        // FOR EF!
    }

    public static Category Create(string name, Guid? parentId, string imageUrl)
    {
        var category = new Category(name, parentId, imageUrl)
        {
            Name = name.Fix(),
            ImageUrl = imageUrl,
            ParentId = ValidateParentId(parentId),
        };

        return category;
    }

    public void Update(string name, Guid? parentId, string imageUrl)
    {
        Name = name.Fix();
        ImageUrl = imageUrl;
        ParentId = ValidateParentId(parentId);
    }

    private static Guid? ValidateParentId(Guid? parentId)
    {
        if (parentId == Guid.Empty)
        {
            parentId = null;
        }

        return parentId;
    }

    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public string ImageUrl { get; private set; }

    private Category(string name, Guid? parentId, string imageUrl)
    {
        Name = name;
        ParentId = parentId;
        ImageUrl = imageUrl;
    }
}
