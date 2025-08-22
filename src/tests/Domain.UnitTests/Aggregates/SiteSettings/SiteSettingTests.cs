using Domain.Aggregates.SiteSettings;
using Xunit;

namespace Domain.UnitTests.Aggregates.SiteSettings;

public class SiteSettingTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateSiteSetting()
    {
        // Arrange
        var description = "Test Description";
        var name = "Test Site";
        var phoneNumber = "09123456789";
        var logoUrl = "https://example.com/logo.png";
        var priceListId = Guid.NewGuid();
        var address = "Test Address";

        // Act
        var siteSetting = SiteSetting.Create(description, name, phoneNumber, logoUrl, priceListId, address);

        // Assert
        Assert.NotNull(siteSetting);
        Assert.Equal(description, siteSetting.Desciption);
        Assert.Equal(name, siteSetting.Name);
        Assert.Equal(phoneNumber, siteSetting.PhoneNumber);
        Assert.Equal(logoUrl, siteSetting.LogoURL);
        Assert.Equal(priceListId, siteSetting.PriceListID);
        Assert.Equal(address, siteSetting.Address);
    }

    [Fact]
    public void Create_WithEmptyPriceListId_ShouldSetToNull()
    {
        // Arrange
        var description = "Test Description";
        var name = "Test Site";
        var phoneNumber = "09123456789";
        var logoUrl = "https://example.com/logo.png";
        var priceListId = Guid.Empty;
        var address = "Test Address";

        // Act
        var siteSetting = SiteSetting.Create(description, name, phoneNumber, logoUrl, priceListId, address);

        // Assert
        Assert.Null(siteSetting.PriceListID);
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateSiteSetting()
    {
        // Arrange
        var siteSetting = SiteSetting.Create(
            "Old Description", "Old Name", "09123456789", 
            "https://old.com/logo.png", Guid.NewGuid(), "Old Address");

        var newDescription = "New Description";
        var newName = "New Name";
        var newPhoneNumber = "09876543210";
        var newLogoUrl = "https://new.com/logo.png";
        var newPriceListId = Guid.NewGuid();
        var newAddress = "New Address";

        // Act
        siteSetting.Update(newDescription, newName, newPhoneNumber, newLogoUrl, newPriceListId, newAddress);

        // Assert
        Assert.Equal(newDescription, siteSetting.Desciption);
        Assert.Equal(newName, siteSetting.Name);
        Assert.Equal(newPhoneNumber, siteSetting.PhoneNumber);
        Assert.Equal(newLogoUrl, siteSetting.LogoURL);
        Assert.Equal(newPriceListId, siteSetting.PriceListID);
        Assert.Equal(newAddress, siteSetting.Address);
    }
}
