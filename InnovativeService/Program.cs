using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InnovativeWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseWindowsService()
    .Build();

host.Run();