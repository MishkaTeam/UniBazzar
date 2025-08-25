namespace Modules.Notification.Application.Settings;

public class SmsSettings
{
    public string Provider { get; set; } = string.Empty;
    public KavenegarSettings Kavenegar { get; set; } = new();
    public MeliPayamakSettings MeliPayamak { get; set; } = new();
}

public class KavenegarSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
}

public class MeliPayamakSettings
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
}