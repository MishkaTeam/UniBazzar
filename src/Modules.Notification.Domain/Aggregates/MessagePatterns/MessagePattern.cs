using BuildingBlocks.Domain.Aggregates;

namespace Modules.Notification.Domain.Aggregates.MessagePatterns;

public class MessagePattern : Entity
{
    private MessagePattern()
    {

    }

    public Guid UserId { get; set; }
    public string Name { get; private set; }
    public string Content { get; private set; }
    public bool IsActive { get; private set; }

    public static MessagePattern Create(string name, string content)
    {
        var messagePattern = new MessagePattern(name, content)
        {
            Name = name,
            Content = content
        };
        return messagePattern;
    }

    public void Update(string name, string content)
    {
        Name = name;
        Content = content;
    }

    private MessagePattern(string name, string content)
    {
        Name = name;
        Content = content;
    }
}
