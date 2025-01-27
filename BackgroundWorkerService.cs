namespace BSChannelRepro;

public class BackgroundWorkerService : BackgroundService
{
    private readonly SyncChannel _channel;
    private readonly ILogger<BackgroundWorkerService> _logger;

    public BackgroundWorkerService(SyncChannel channel, ILogger<BackgroundWorkerService> logger)
    {
        _channel = channel;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await foreach (var message in _channel.GetReader().ReadAllAsync(stoppingToken))
            {
                // simulate work
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                _logger.LogInformation("Got message: {Message}!", message);
                _channel.GetHistory().Add(new SyncItem(DateTime.Now, message));
            }
        }
    }
}