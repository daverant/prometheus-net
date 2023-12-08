using MassTransit;
using Prometheus;
using PrometheusNetRepro;

using var metricServer = PrometheusServer.Start();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<DotNetMetersService>();
builder.Services.AddHostedService<MassTransitService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MassTransitConsumer>();
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseRouting();
app.UseHttpMetrics();

await app.RunAsync();