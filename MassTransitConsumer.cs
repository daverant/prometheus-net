using MassTransit;

namespace PrometheusNetRepro;

internal sealed class MassTransitConsumer : IConsumer<MassTransitMessage>
{
    readonly ILogger<MassTransitConsumer> _logger;

    public MassTransitConsumer(ILogger<MassTransitConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<MassTransitMessage> context)
    {
        _logger.LogInformation("Received Text: {Text}", context.Message.Value);
        return Task.CompletedTask;
    }
}