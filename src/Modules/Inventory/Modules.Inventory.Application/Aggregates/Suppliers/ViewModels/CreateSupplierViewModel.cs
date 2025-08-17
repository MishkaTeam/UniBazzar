using Modules.Inventory.Domain.Aggreates.Suppliers.Enums;

namespace Modules.Inventory.Application.Aggregates.Suppliers.ViewModels;

public class CreateSupplierViewModel
{
    public string FullName { get; set; }

    public SupplierType SupplierType { get; set; }

    public string? NationalID { get; private set; }

    public string? EconomicCode { get; private set; }

    public string? Phone { get; private set; }

    public string? Address { get; private set; }
}
