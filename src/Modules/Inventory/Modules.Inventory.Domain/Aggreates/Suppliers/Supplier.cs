using BuildingBlocks.Domain.Aggregates;
using Modules.Inventory.Domain.Aggreates.Suppliers.Enums;

namespace Modules.Inventory.Domain.Aggreates.Suppliers
{
    public class Supplier : Entity
    {
        public string FullName { get; private set; }

        public string? NationalID { get; private set; }

        public string? EconomicCode { get; private set; }

        public string? Phone { get; private set; }

        public string? Address { get; private set; }

        public SupplierType SupplierType { get; private set; }


        private Supplier(string fullName, string? nationalID, string? economicCode, string? phone, string? address, SupplierType supplierType)
        {
            if (supplierType == SupplierType.Individual && string.IsNullOrWhiteSpace(nationalID))
                throw new ArgumentException("NationalID is required for Individual counterparties.");
            if (supplierType == SupplierType.Corporate && string.IsNullOrWhiteSpace(economicCode))
                throw new ArgumentException("EconomicCode is required for Corporate counterparties.");

            FullName = fullName;
            NationalID = nationalID;
            EconomicCode = economicCode;
            Phone = phone;
            Address = address;
            SupplierType = supplierType;
        }

        public static Supplier Create(string fullName, string? nationalID, string? economicCode, string? phone, string? address, SupplierType supplierType)
        {
            return new Supplier(fullName, nationalID, economicCode, phone, address, supplierType);
        }

        public void UpdateDetails(string fullName, string? nationalID, string? economicCode, string? phone, string? address, SupplierType supplierType)
        {
            if (supplierType == SupplierType.Individual && string.IsNullOrWhiteSpace(nationalID))
                throw new ArgumentException("NationalID is required for Individual counterparties.");
            if (supplierType == SupplierType.Corporate && string.IsNullOrWhiteSpace(economicCode))
                throw new ArgumentException("EconomicCode is required for Corporate counterparties.");

            FullName = fullName;
            NationalID = nationalID;
            EconomicCode = economicCode;
            Phone = phone;
            Address = address;
            SupplierType = supplierType;
        }
    }
}
