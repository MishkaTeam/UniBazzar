using Domain.Aggregates.Units;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Units
{
    public class UnitTests
    {
        [Fact]
        public void Create_WithBaseUnitAndValidRatio_ShouldSetProperties()
        {

            string title = "Child Unit";
            var baseUnitUnit = Unit.Create("BaseUnit Unit", null, 1m);
            decimal ratio = 0.5m;


            var childUnit = Unit.Create(title, baseUnitUnit.Id, ratio);


            childUnit.Title.Should().Be(title);
            childUnit.BaseUnitId.Should().Be(baseUnitUnit.Id);
            childUnit.Ratio.Should().Be(ratio);
        }

        [Fact]
        public void Create_WithoutBaseUnit_ShouldSetRatioToOne()
        {

            string title = "Standalone Unit";


            var unit = Unit.Create(title, null);


            unit.Title.Should().Be(title);
            unit.BaseUnit.Should().BeNull();
            unit.Ratio.Should().Be(1m); // Ratio should be 1 when no baseUnit is provided
        }

        [Fact]
        public void Create_WithNegativeOrZeroRatio_ShouldThrowArgumentException()
        {

            string title = "Invalid Unit";
            var baseUnitUnit = Unit.Create("BaseUnit Unit", null, 1m);
            decimal invalidRatio = -1m;


            Action action = () => Unit.Create(title, baseUnitUnit.Id, invalidRatio);


            var message = string.Format(
                    Resources.Messages.Validations.GreaterThan
                    , Resources.DataDictionary.UnitRatio, "0");
            action.Should().Throw<ArgumentException>().WithMessage(message);
        }
    }

}