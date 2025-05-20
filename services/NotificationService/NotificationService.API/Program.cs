using MassTransit;
using NotificationService.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// MassTransit ve RabbitMQ konfigürasyonu
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EventCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("event-created-queue", e =>
        {
            e.ConfigureConsumer<EventCreatedConsumer>(context);
        });
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
