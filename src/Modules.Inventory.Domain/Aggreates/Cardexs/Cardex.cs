using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Products;
using Domain.Aggregates.Units;
using System.Security.AccessControl;

namespace Modules.Inventory.Domain.Aggreates.Cardexs
{
    public class Cardex : Entity
    {
        public Guid InventoryId { get; set; }
        public Inventories.Inventory Inventory { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public decimal UnitPrice { get; set; }

        public string ReferenceNumber { get; set; }

        public decimal QuantityIn { get; set; }

        public decimal QuantityOut { get; set; }

        public string? Description { get; set; }

        private Cardex(Guid inventoryId, Guid productId, Guid unitId, decimal unitPrice,
                       string referenceNumber, decimal quantityIn, decimal quantityOut, string? description)
        {
            InventoryId = inventoryId;
            ProductId = productId;
            UnitId = unitId;
            UnitPrice = unitPrice;
            ReferenceNumber = referenceNumber;
            QuantityIn = quantityIn;
            QuantityOut = quantityOut;
            Description = description;
        }

        public static Cardex Create(Guid inventoryId, Guid productId, Guid unitId, decimal unitPrice,
                       string referenceNumber, decimal quantityIn, decimal quantityOut, string? description)
        {
            return new Cardex(inventoryId, productId, unitId, unitPrice, referenceNumber, quantityIn, quantityOut, description);
        }

        public void Update(Guid inventoryId, Guid productId, Guid unitId, decimal unitPrice,
                       string referenceNumber, decimal quantityIn, decimal quantityOut, string? description)
        {
            InventoryId = inventoryId;
            ProductId = productId;
            UnitId = unitId;
            UnitPrice = unitPrice;
            ReferenceNumber = referenceNumber;
            QuantityIn = quantityIn;
            QuantityOut = quantityOut;
            Description = description;
        }
    }
}
