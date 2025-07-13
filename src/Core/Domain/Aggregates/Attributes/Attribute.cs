using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Categories;
using Framework.DataType;

namespace Domain.Aggregates.Attributes;

public class Attribute : Entity
{
    public Attribute()
    {

    }


    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public List<AttributeValue> AttributeValues { get; private set; }


    public static Attribute Create(string name, string description, Guid categoryId)
    {
        var attributes = new Attribute(name, description, categoryId)
        {
            Name = name.Fix(),
            Description = description.Fix(),
            CategoryId = categoryId
        };
        return attributes;
    }
    public void Update(string name, string description, Guid categoryId)
    {
        Name = name.Fix();
        Description = description.Fix();
        CategoryId=categoryId;
    }
    public void AddValue(AttributeValue item)
    {
        AttributeValues.Add(item);
    }

    private Attribute(string name, string description, Guid categoryId)
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
    } 
}
