using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAppMonitoramentoWebhook.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhookMonitorController : ControllerBase
{
    private readonly ILogger<WebhookMonitorController> _logger;
    private static object? _lastEvent;

    public WebhookMonitorController(ILogger<WebhookMonitorController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public object? GetLastEvent()
    {
        _logger.LogInformation($"{nameof(GetLastEvent)} | Último evento recebido: " +
            JsonSerializer.Serialize(_lastEvent,
                options: new() { WriteIndented = true }));
        return _lastEvent;
    }

    [HttpPost]
    public IActionResult PostEvent(object data)
    {
        _lastEvent = data;
        _logger.LogInformation($"{nameof(PostEvent)} | Notificação recebida: " +
            JsonSerializer.Serialize(data,
                options: new() { WriteIndented = true }));
        return Ok();
    }
}