using BuildingBlocks.Domain.Aggregates;

namespace Modules.Inventory.Domain.Aggreates.Inventories
{
    public class Inventory : Entity
    {
        public string Name { get; set; }
        public string? Location { get; set; }

        private Inventory(string name, string? location)
        {
            Name = name;
            Location = location;
        }

        public static Inventory Create(string name, string? location)
        {
            var inventory = new Inventory(name, location)
            {
                Name = name,
                Location = location,
            };

            return inventory;
        }

        public void Update(string name, string? location) 
        {
            Name = name;
            Location = location;
        }

    }
}
