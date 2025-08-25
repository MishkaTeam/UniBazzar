using Microsoft.AspNetCore.Mvc;
using Modules.Notification.Application.Contracts;

namespace Modules.Notification.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SmsController : ControllerBase
{
    private readonly ISendSms _sendSms;

    public SmsController(ISendSms sendSms)
    {
        _sendSms = sendSms;
    }

    [HttpPost("send-otp")]
    public IActionResult SendOtp([FromQuery] string number, [FromQuery] string otp)
    {
        if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(otp))
            return BadRequest("شماره یا کد وارد نشده است.");

        try
        {
            _sendSms.SendOtpAsync(number, otp);
            return Ok("پیامک با موفقیت ارسال شد.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"خطا در ارسال پیامک: {ex.Message}");
        }
    }
}
