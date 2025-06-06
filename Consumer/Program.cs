using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using GeoSolution.Shared;           
using GeoSolution.Consumer;        

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

        services.AddScoped<UserRegisteredEventHandler>();
        services.AddScoped<NotificationManager>();
        services.AddSingleton<INotificationSender, EmailNotificationSender>();
        services.AddSingleton<INotificationSender, SmsNotificationSender>();
        services.AddSingleton<INotificationSender, AdminEmailNotificationSender>();

        services.AddHostedService<RabbitMqNotificationConsumer>();
    })
    .Build()
    .Run();
