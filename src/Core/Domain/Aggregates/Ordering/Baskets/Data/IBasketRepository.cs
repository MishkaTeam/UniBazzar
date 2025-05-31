using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.Ordering.Baskets.Data;

public interface IBasketRepository : IRepositoryBase<Basket>
{
    Task<Basket?> GetWithItemsByIdAsync(Guid requestBasketId);
    Task<Basket?> GetWithItemsByReferenceNumberAsync(string referenceNumber);
}