using MassTransit;
using NotificationService.API.Consumers;
using NotificationService.Infrastructure.Services;
using NotificationService.Infrastructure.Settings;

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

builder.Services.AddHttpClient<IUserInfoService, UserInfoService>(client =>
{
    client.BaseAddress = new Uri("http://authservice-api:8080");
});
builder.Services.AddHttpClient<IMembershipInfoService, MembershipInfoService>(client =>
{
    client.BaseAddress = new Uri("http://membershipservice-api:8080");
});
builder.Services.Configure<MailSettings>(options =>
{
    builder.Configuration.GetSection("MailSettings").Bind(options);

    // Þifreyi ortam deðiþkeninden al
    options.Password = Environment.GetEnvironmentVariable("MAIL_PASSWORD");
});

builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
