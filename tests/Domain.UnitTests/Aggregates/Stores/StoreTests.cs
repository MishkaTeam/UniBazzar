using Domain.Aggregates.Stores;
using FluentAssertions;
using Resources;
using Resources.Messages;

namespace Domain.UnitTests.Aggregates.Stores;

public class StoreTests
{
	[Fact]
	public void Create_WithValidProperties_Should_SetProperties()
	{
		string name = "Store Name";
		string? description = "Store Description";
		string phoneNumber = "09000000000";
		string address = "Store Address";
		string? culture = "fa-Ir";
		string? logoUrl = "logo/test.png";
		bool isActive = true;


		var store = Store.Create(
			name, description, phoneNumber,
			address, culture, logoUrl, isActive);


		store.Name.Should().Be(name);
		store.Description.Should().Be(description);
		store.PhoneNumber.Should().Be(phoneNumber);
		store.Address.Should().Be(address);
		store.Culture.Should().Be(culture);
		store.LogoUrl.Should().Be(logoUrl);
		store.IsActive.Should().Be(isActive);
	}

	[Fact]
	public void Create_WithInvalidPhonenNumber_Should_ThrowInvalidDataException()
	{
		string name = "Store Name";
		string? description = "Store Description";
		string phoneNumber = "INVALID_PhoneNumber";
		string address = "Store Address";
		string? culture = "fa-Ir";
		string? logoUrl = "logo/test.png";
		bool isActive = true;


		Action action = () => Store.Create(
			name, description, phoneNumber,
			address, culture, logoUrl, isActive);


		var message = string.Format(
			Errors.Invalid, DataDictionary.CellPhonenumber);

		action.Should().Throw<InvalidDataException>().WithMessage(message);
	}
}