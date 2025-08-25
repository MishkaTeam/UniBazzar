
using BuildingBlocks.Domain.Aggregates;

namespace Modules.Notification.Domain.Aggregates.MessageLogs;

public class MessageLog : Entity
{
    private MessageLog()
    {

    }
    public Guid UserId { get; private set; }
    public string Mobile { get; private set; }
    public string Content { get; private set; }
    public DateTime? SentAt { get; private set; }

    public static MessageLog Create(string mobile, string content)
    {
        var messagePattern = new MessageLog(mobile, content)
        {
            Mobile = mobile,
            Content = content,
        };
        return messagePattern;
    }

    public void Update(string mobile, string content)
    {
        Mobile = mobile;
        Content = content;
    }
    private MessageLog(string mobile, string content)
    {
        Mobile = mobile;
        Content = content;
    }

}
