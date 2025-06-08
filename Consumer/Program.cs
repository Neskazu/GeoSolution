using Microsoft.Extensions.Options;
using GeoSolution.Consumer.Services.Notification;
using GeoSolution.Shared.Options;
using GeoSolution.Consumer.Services.Messaging;
using RabbitMQ.Client;
using GeoSolution.Shared.Services.Messaging.Abstractions;
using NETCore.MailKit.Core;
using GeoSolution.Consumer.Services.Email;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<RabbitMqOptions>(
            hostContext.Configuration.GetSection("RabbitMQ"));

        services.AddSingleton<IConnectionFactory>(sp =>
        {
            var opts = sp.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
            return new ConnectionFactory
            {
                HostName = opts.Host,
                Port = opts.Port,
                UserName = opts.Username,
                Password = opts.Password,
                VirtualHost = opts.VirtualHost
            };
        });
        services.AddSingleton<IEmailService, MailKitEmailService>();
        services.AddSingleton<NotificationManager>();
        services.Configure<SmtpSettings>(hostContext.Configuration.GetSection("SmtpSettings"));
        services.Configure<DeffaultQueueOptions>(hostContext.Configuration.GetSection("Queues:DeffaultQueue"));
        services.AddSingleton<INotificationSender, EmailNotificationSender>();
        services.AddSingleton<INotificationSender, SmsNotificationSender>();
        services.AddSingleton<INotificationSender, AdminEmailNotificationSender>();
        services.AddSingleton<UserRegisteredEventHandler>();

        services.AddSingleton<Dictionary<string, IEventHandler>>(sp =>
        {
            var dict = new Dictionary<string, IEventHandler>(StringComparer.OrdinalIgnoreCase)
            {
                { "UserRegistered", sp.GetRequiredService<UserRegisteredEventHandler>() }
               //new can be added easily
            };
            return dict;
        });
        services.AddHostedService<RabbitMqNotificationConsumer>();
    })
    .Build()
    .Run();
