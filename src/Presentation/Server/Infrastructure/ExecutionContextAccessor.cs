using Domain.Aggregates.Stores;
using Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Server.Infrastructure
{
    public class ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor, IStoreRepository storeRepository) : IExecutionContextAccessor
    {
        public Guid? UserId
        {
            get
            {
                var user = httpContextAccessor.HttpContext?.User;

                if (user?.Identity is { IsAuthenticated: true })
                {
                    var userIdClaim = user.FindFirst(ClaimTypes.Sid)?.Value;

                    if (Guid.TryParse(userIdClaim, out var userId))
                        return userId;

                    throw new Exception("UserId claim is missing or invalid");
                }

                return null;
            }
        }
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
