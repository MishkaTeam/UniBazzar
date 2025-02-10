using Domain.Aggregates.Categories;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Categories;

public class CategoriesTests
{
    [Fact]
    public void Create_WithParentIdandValidProperties_ShouldSetProperties()
    {
        string name = "name";
        string imageurl = "test.png";
        Guid parentid = Guid.NewGuid();

        var category = Category.Create(name, parentid, imageurl);

        category.Name.Should().Be(name);
        category.ParentId.Should().Be(parentid);
        category.IconClass.Should().Be(imageurl);
    }

    [Fact]
    public void Create_WithoutParentIdandValidProperties_ShouldSetProperties()
    {
        string name = "name";
        string imageurl = "test.png";
        Guid parentid = Guid.Empty;

        var category = Category.Create(name, parentid, imageurl);

        category.Name.Should().Be(name);
        category.ParentId.Should().BeNull();
        category.IconClass.Should().Be(imageurl);
    }
}
