using BuildingBlocks.Persistence;
using Domain.Aggregates.Attributes.Data;

namespace Persistence.Repositories.Aggregates.Attributes;

public class AttributesRepository
    (UniBazzarContext uniBazzarContext, IExecutionContextAccessor executionContextAccessor)
    : RepositoryBase<Domain.Aggregates.Attributes.Attribute>(uniBazzarContext, executionContextAccessor), IAttributeRepository
{

}

