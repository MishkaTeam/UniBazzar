using Microsoft.Extensions.Options;
using Modules.Notification.Application.Contracts;
using Modules.Notification.Application.Notifications.SmsSender;
using Modules.Notification.Application.Settings;
using Modules.Notification.Provider.Kavenegar;
using Modules.Notification.Provider.MeliPayamak;
namespace Modules.Notification.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;


        builder.Services.Configure<SmsSettings>(builder.Configuration.GetSection("SmsSettings"));

        // Register provider
        builder.Services.AddScoped<ISendSms>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<SmsSettings>>().Value;

            return settings.Provider switch
            {
                "Kavenegar" => new KavenegarSmsService(sp.GetRequiredService<IOptions<SmsSettings>>()),
                "MeliPayamak" => new MeliPayamakSmsService(sp.GetRequiredService<IOptions<SmsSettings>>()),
                _ => throw new Exception("Invalid SMS provider configuration.")
            };
        });


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
       

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
