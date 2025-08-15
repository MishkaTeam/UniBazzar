using BuildingBlocks.Domain.Aggregates;

namespace Modules.Inventory.Domain.Aggreates.Warehouses
{
    public class Warehouse : Entity
    {
        public string Name { get; set; }
        public string? Location { get; set; }

        private Warehouse(string name, string? location)
        {
            Name = name;
            Location = location;
        }

        public static Warehouse Create(string name, string? location)
        {
            var inventory = new Warehouse(name, location)
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
