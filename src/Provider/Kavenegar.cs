using Kavenegar;
using Kavenegar.Core.Exceptions;
using Microsoft.Extensions.Options;
using Modules.Notification.Application.Contracts;
using Modules.Notification.Application.Settings;

namespace Modules.Notification.Provider.Kavenegar;


public class KavenegarSmsService : ISendSms
{
    private readonly string _apiKey;
    private readonly string _sender;

    public KavenegarSmsService(IOptions<SmsSettings> settings)
    {
        _apiKey = settings.Value.Kavenegar.ApiKey;
        _sender = settings.Value.Kavenegar.Sender;
    }

    public async Task SendOtpAsync(string number, string otp)
    {
        try
        {
            var api = new KavenegarApi(_apiKey);
            var result = await api.Send(_sender, number, otp);

            Console.WriteLine($"[Kavenegar] MessageId: {result.Messageid}, Status: {result.Status}");
        }
        catch (ApiException ex)
        {
            Console.WriteLine("API Error: " + ex.Message);
            throw;
        }
        catch (HttpException ex)
        {
            Console.WriteLine("HTTP Error: " + ex.Message);
            throw;
        }
    }
}

