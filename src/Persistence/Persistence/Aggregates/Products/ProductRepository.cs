using BuildingBlocks.Persistence;
using Domain.Aggregates.Products;

namespace Persistence.Aggregates.Products;

public partial class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
    }
}