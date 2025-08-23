using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Products;
using Domain.Aggregates.Units;
using Modules.Inventory.Domain.Aggreates.Cardexs.Enums;
using Modules.Inventory.Domain.Aggreates.Warehouses;
using System.Security.AccessControl;

namespace Modules.Inventory.Domain.Aggreates.Cardexs
{
    public class Cardex : Entity
    {
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public decimal UnitPrice { get; set; }

        public CardexType CardexType { get; set; }

        public string ReferenceNumber { get; set; }

        public decimal QuantityIn { get; set; }

        public decimal QuantityOut { get; set; }

        public string? Description { get; set; }

        public decimal Balance => QuantityIn - QuantityOut;

        public DateTime Date { get; set; }

        private Cardex(Guid warehouseId, Guid productId, Guid unitId, decimal unitPrice, CardexType cardexType,
                       string referenceNumber, decimal quantityIn, decimal quantityOut, string? description, DateTime date)
        {
            WarehouseId = warehouseId;
            ProductId = productId;
            UnitId = unitId;
            UnitPrice = unitPrice;
            CardexType = cardexType;
            ReferenceNumber = referenceNumber;
            QuantityIn = quantityIn;
            QuantityOut = quantityOut;
            Description = description;
            Date = date;
        }

        public static Cardex Create(Guid warehouseId, Guid productId, Guid unitId, decimal unitPrice, CardexType cardexType,
                       string referenceNumber, decimal quantityIn, decimal quantityOut, string? description, DateTime date)
        {
            return new Cardex(warehouseId, productId, unitId, unitPrice, cardexType, referenceNumber,
                              quantityIn, quantityOut, description, date);
        }

        public void Update(Guid warehouseId, Guid productId, Guid unitId, decimal unitPrice, CardexType cardexType,
                       string referenceNumber, decimal quantityIn, decimal quantityOut, string? description, DateTime date)
        {
            WarehouseId = warehouseId;
            ProductId = productId;
            UnitId = unitId;
            UnitPrice = unitPrice;
            CardexType = cardexType;
            ReferenceNumber = referenceNumber;
            QuantityIn = quantityIn;
            QuantityOut = quantityOut;
            Description = description;
            Date = date;
        }
    }
}
