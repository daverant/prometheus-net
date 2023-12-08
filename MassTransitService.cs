using MassTransit;

namespace PrometheusNetRepro;

internal sealed class MassTransitService : BackgroundService
{
    private readonly IBus _bus;

    public MassTransitService(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new MassTransitMessage { Value = $"The time is {DateTimeOffset.Now}" }, stoppingToken);

            await Task.Delay(100, stoppingToken);
        }
    }
}