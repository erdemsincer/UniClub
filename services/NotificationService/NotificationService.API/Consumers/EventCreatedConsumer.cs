using MassTransit;
using NotificationService.Infrastructure.Services;
using UniClub.Shared.Events;

namespace NotificationService.API.Consumers;

public class EventCreatedConsumer : IConsumer<EventCreatedEvent>
{
    private readonly IMembershipInfoService _membershipInfoService;
    private readonly IUserInfoService _userInfoService;
    private readonly IMailService _mailService;

    public EventCreatedConsumer(
        IMembershipInfoService membershipInfoService,
        IUserInfoService userInfoService,
        IMailService mailService)
    {
        _membershipInfoService = membershipInfoService;
        _userInfoService = userInfoService;
        _mailService = mailService;
    }

    public async Task Consume(ConsumeContext<EventCreatedEvent> context)
    {
        var message = context.Message;
        Console.WriteLine($"📨 Etkinlik oluşturuldu: {message.Title} - {message.StartTime}");

        // 1. Kulüp üyelerinin userId'lerini al
        var userIds = await _membershipInfoService.GetUserIdsByClubIdAsync(message.ClubId);

        foreach (var userId in userIds)
        {
            // 2. Email adresini al
            var email = await _userInfoService.GetEmailByUserIdAsync(userId);
            if (string.IsNullOrWhiteSpace(email))
                continue;

            // 3. Eposta gönder
            var subject = $"🎉 Yeni Etkinlik: {message.Title}";



            var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f4f1ea; padding: 20px;'>
    <div style='max-width: 600px; margin: auto; background-color: #fff; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.08); padding: 30px;'>
        <h2 style='color: #8c7051; border-bottom: 2px solid #8c7051; padding-bottom: 10px;'>📢 {message.Title}</h2>
        
        <p style='font-size: 16px; color: #5e4b3c;'>
            <strong>📅 Tarih:</strong> {message.StartTime:dd MMMM yyyy HH:mm}
        </p>

        <p style='font-size: 16px; color: #5e4b3c;'>
            <strong>📝 Açıklama:</strong><br />
            {message.Description}
        </p>

        <p style='font-size: 16px; color: #5e4b3c;'>
            <strong>🏛️ Kulüp No:</strong> #{message.ClubId}
        </p>

        <div style='margin-top: 30px; border-top: 1px solid #ccc; padding-top: 15px;'>
            <p style='font-size: 12px; color: #999;'>
                Bu e-posta <strong>UniClub</strong> platformu tarafından otomatik olarak gönderilmiştir.
            </p>
        </div>
    </div>
</body>
</html>";



            await _mailService.SendEmailAsync(email, subject, body);

            Console.WriteLine($"✅ Mail gönderildi → {email}");
        }
    }
    }
