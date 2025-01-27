using Microsoft.AspNetCore.Mvc;

namespace BSChannelRepro.Controllers;

public class SyncMessage
{
    public string Message { get; set; } = null!;
}

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private readonly SyncChannel _channel;

    public HomeController(SyncChannel channel)
    {
        _channel = channel;
    }

    [HttpGet]
    public ActionResult<List<SyncItem>> Index()
    {
        return Ok(_channel.GetHistory());
    }

    [HttpPost("/queue")]
    public async Task<IActionResult> QueueMessage([FromBody] SyncMessage sync)
    {
        await _channel.GetWriter().WriteAsync(sync.Message);
        return Accepted(new
        {
            Message = $"There are {_channel.GetReader().Count} item(s) in the queue."
        });
    }
}