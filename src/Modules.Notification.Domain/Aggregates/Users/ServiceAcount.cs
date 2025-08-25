using BuildingBlocks.Domain.Aggregates;
using Modules.Notification.Domain.Aggregates.MessageLogs;
using Modules.Notification.Domain.Aggregates.MessagePatterns;

namespace Modules.Notification.Domain.Aggregates.Users;

public class ServiceAcount : Entity
{
    private ServiceAcount()
    {
        // EF Core
    }

    public string Name { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public List<MessageLog> MessageLogs { get; private set; }
    public List<MessagePattern> MessagePatterns { get; set; }
}

