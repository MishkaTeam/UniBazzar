using Domain.BuildingBlocks.Aggregates;
using Framework.DataType;

namespace Domain.Aggregates.branches
{
    public class Branch : Entity
    {
        public Branch()
        {
            // FOR EF!
        }

        public string Name { get; private set; }

        public static Branch Create(string name)
        {
            var branch = new Branch(name)
            {
                Name = name.Fix()
            };
            return branch;
        }

        public Branch Update(string name)
        {
            var branch = new Branch(name)
            {
                Name = name
            };
            return branch;
        }

        private Branch(string name)
        {
            Name = name;
        }

    }
}
