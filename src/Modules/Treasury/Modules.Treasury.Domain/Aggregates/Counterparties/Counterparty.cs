using BuildingBlocks.Domain.Aggregates;
using Modules.Treasury.Domain.Aggregates.Counterparties.Enums;

namespace Modules.Treasury.Domain.Aggregates.Counterparties;

public class Counterparty : Entity
{
    public string FullName { get; private set; }

    public string? NationalID { get; private set; }

    public string? EconomicCode { get; private set; }

    public string? Phone { get; private set; }

    public string? Address { get; private set; }

    public CounterpartyType CounterpartyType { get; private set; }

    public Guid? SourceCustomerID { get; private set; }

    private Counterparty(
        string fullName,
        string? nationalID,
        string? economicCode,
        string? phone,
        string? address,
        CounterpartyType counterpartyType,
        Guid? sourceCustomerID)
    {
        if (counterpartyType == CounterpartyType.Individual && string.IsNullOrWhiteSpace(nationalID))
            throw new ArgumentException("NationalID is required for Individual counterparties.");
        if (counterpartyType == CounterpartyType.Corporate && string.IsNullOrWhiteSpace(economicCode))
            throw new ArgumentException("EconomicCode is required for Corporate counterparties.");

        FullName = fullName;
        NationalID = nationalID;
        EconomicCode = economicCode;
        Phone = phone;
        Address = address;
        CounterpartyType = counterpartyType;
        SourceCustomerID = sourceCustomerID;
    }

    public static Counterparty CreateFromSales(
        Guid sourceCustomerID,
        string fullName,
        CounterpartyType counterpartyType,
        string? nationalID = null,
        string? economicCode = null,
        string? phone = null,
        string? address = null)
    {
        return new Counterparty(
            fullName,
            nationalID,
            economicCode,
            phone,
            address,
            counterpartyType,
            sourceCustomerID);
    }


    public void UpdateDetails(
        string fullName,
        string? nationalID,
        string? economicCode,
        string? phone,
        string? address,
        CounterpartyType counterpartyType)
    {
        if (counterpartyType == CounterpartyType.Individual && string.IsNullOrWhiteSpace(nationalID))
            throw new ArgumentException("NationalID is required for Individual counterparties.");
        if (counterpartyType == CounterpartyType.Corporate && string.IsNullOrWhiteSpace(economicCode))
            throw new ArgumentException("EconomicCode is required for Corporate counterparties.");

        FullName = fullName;
        NationalID = nationalID;
        EconomicCode = economicCode;
        Phone = phone;
        Address = address;
        CounterpartyType = counterpartyType;
    }
}
