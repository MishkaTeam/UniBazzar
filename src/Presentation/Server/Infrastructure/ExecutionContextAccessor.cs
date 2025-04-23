using Domain.Aggregates.Stores;
using Persistence;

namespace Server.Infrastructure
{
    public class ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor, IStoreRepository storeRepository) : IExecutionContextAccessor
    {
        public Guid UserId { get => Guid.NewGuid(); }
        public Guid StoreId
        {
            get
            {

                var hostUrl = httpContextAccessor.HttpContext?.Request.Host.Value;
                if (hostUrl is null)
                    throw new Exception("Host URL is null");

                //if (hostUrl == "localhost:7052")
                //    return Guid.Parse("00000000-0000-0000-0000-000000000001");

                return storeRepository.GetStoreByHostUrl(hostUrl);
            }
        }
    }
}
