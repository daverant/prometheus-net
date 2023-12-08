using Prometheus;
using Prometheus.DotNetRuntime;

namespace PrometheusNetRepro;

internal sealed class PrometheusServer : IDisposable
{
    private readonly IDisposable _dotNetRuntimeStatsBuilder;
    private readonly KestrelMetricServer _metricServer;

    private PrometheusServer()
    {
        var hostName = Environment.OSVersion.Platform == PlatformID.Unix ? "*" : "localhost";
        _dotNetRuntimeStatsBuilder = DotNetRuntimeStatsBuilder.Default().StartCollecting();
        _metricServer = new KestrelMetricServer(hostName, 1234);

        _metricServer.Start();
    }

    public static IDisposable Start()
    {
        return new PrometheusServer();
    }

    public void Dispose()
    {
        _dotNetRuntimeStatsBuilder.Dispose();
        _metricServer.Stop();
    }
}