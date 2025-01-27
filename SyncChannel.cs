using System.Threading.Channels;

namespace BSChannelRepro;


public record SyncItem(DateTime Timestamp, string Message);
public class SyncChannel
{
    private readonly Channel<string> _channel = Channel.CreateUnbounded<string>();
    private readonly ILogger<SyncChannel> _logger;
    private readonly List<SyncItem> _history = [];

    public SyncChannel(ILogger<SyncChannel> logger)
    {
        _logger = logger;
    }

    public ChannelWriter<string> GetWriter()
    {
        _logger.LogInformation("Channel writer accessed!");
        return _channel.Writer;
    }

    public ChannelReader<string> GetReader() => _channel.Reader;
    
    public List<SyncItem> GetHistory() => _history;
}