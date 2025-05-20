using MassTransit;
using UniClub.Shared.Events;

namespace NotificationService.API.Consumers;

public class EventCreatedConsumer : IConsumer<EventCreatedEvent>
{
    public Task Consume(ConsumeContext<EventCreatedEvent> context)
    {
        var message = context.Message;
        Console.WriteLine($"📨 Etkinlik Oluşturuldu: {message.Title} - {message.StartTime}");
        return Task.CompletedTask;
    }
}
