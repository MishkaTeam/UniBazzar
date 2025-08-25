using Microsoft.Extensions.Options;
using Modules.Notification.Application.Contracts;
using Modules.Notification.Application.Settings;

namespace Modules.Notification.Provider.MeliPayamak;

public class MeliPayamakSmsService : ISendSms
{
    private readonly string _username;
    private readonly string _password;
    private readonly string _sender;

    public MeliPayamakSmsService(IOptions<SmsSettings> settings)
    {
        _username = settings.Value.MeliPayamak.Username;
        _password = settings.Value.MeliPayamak.Password;
        _sender = settings.Value.MeliPayamak.Sender;
    }

    public Task SendOtpAsync(string number, string otp)
    {
        // TODO: cod sdk melipayamak
        Console.WriteLine($"[MeliPayamak] Sending OTP '{otp}' to {number}");
        return Task.CompletedTask;
    }
}