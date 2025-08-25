namespace Modules.Notification.Application.Contracts;
public interface ISendSms
{
    Task SendOtpAsync(string number, string otp);
}
