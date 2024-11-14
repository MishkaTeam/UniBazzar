using Domain.Aggregates.Units;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Units
{
    public class UnitTests
    {
        [Fact]
        public void Create_WithParentAndValidRatio_ShouldSetProperties()
        {

            string title = "Child Unit";
            var parentUnit = Unit.Create("Parent Unit", null, 1m);
            decimal ratio = 0.5m;


            var childUnit = Unit.Create(title, parentUnit, ratio);


            childUnit.Title.Should().Be(title);
            childUnit.Parent.Should().Be(parentUnit);
            childUnit.Ratio.Should().Be(ratio);
        }

        [Fact]
        public void Create_WithoutParent_ShouldSetRatioToOne()
        {

            string title = "Standalone Unit";


            var unit = Unit.Create(title, null);


            unit.Title.Should().Be(title);
            unit.Parent.Should().BeNull();
            unit.Ratio.Should().Be(1m); // Ratio should be 1 when no parent is provided
        }

        [Fact]
        public void Create_WithNegativeOrZeroRatio_ShouldThrowArgumentException()
        {

            string title = "Invalid Unit";
            var parentUnit = Unit.Create("Parent Unit", null, 1m);
            decimal invalidRatio = -1m;


            Action action = () => Unit.Create(title, parentUnit, invalidRatio);


            var message = string.Format(
                    Resources.Messages.Validations.GreaterThan
                    , Resources.DataDictionary.UnitRatio, "0");
            action.Should().Throw<ArgumentException>().WithMessage(message);
        }
    }

}