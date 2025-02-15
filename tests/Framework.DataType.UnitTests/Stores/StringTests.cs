using Domain.Aggregates.Stores;
using FluentAssertions;

namespace Framework.DataType.UnitTests.Stores;

public class StringTests
{
	[Fact]
	public void Create_WithSpaceInProperties_Should_FixSpaces()
	{
		string name = "Store   Name";
		string? description = "Store   Description";
		string phoneNumber = "09000000000";
		string address = "Store   Address";
		string? culture = "fa-Ir";
		string? logoUrl = "logo/test.png";
		bool isActive = true;

		string fixedName = "Store Name";
		string fixedDescription = "Store Description";
		string fixedAddress = "Store Address";


		var store = Store.Create(
			name, description, phoneNumber,
			address, culture, logoUrl, isActive);


		store.Name.Should().Be(fixedName);
		store.Description.Should().Be(fixedDescription);
		store.Address.Should().Be(fixedAddress);
	}

	[Fact]
	public void Create_WithWhiteSpaceProperties_Should_FixEmpty()
	{
		string name = "   ";
		string? description = "   ";
		string phoneNumber = "09000000000";
		string address = "   ";
		string? culture = "fa-Ir";
		string? logoUrl = "logo/test.png";
		bool isActive = true;

		string fixedName = "";
		string fixedDescription = "";
		string fixedAddress = "";


		var store = Store.Create(
			name, description, phoneNumber,
			address, culture, logoUrl, isActive);


		store.Name.Should().Be(fixedName);
		store.Description.Should().Be(fixedDescription);
		store.Address.Should().Be(fixedAddress);
	}
}