using System.Security.Claims;
using BuildingBlocks.Domain.Context;

namespace Server.Infrastructure
{
    public class ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor) : IExecutionContextAccessor
    {

        public string Role
        {
            get
            {

                var user = httpContextAccessor.HttpContext?.User;

                if (user?.Identity is { IsAuthenticated: true })
                {
                    var roleIdClaim = user.FindFirst(ClaimTypes.Role)?.Value;

                    return roleIdClaim ?? "";
                }

                return "";

            }
        }
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
        public Guid StoreId =>
                      Guid.Parse(httpContextAccessor.HttpContext?.Items["TenantId"]?.ToString() ??
                      "00000000-0000-0000-0000-000000000000");
    }
}
