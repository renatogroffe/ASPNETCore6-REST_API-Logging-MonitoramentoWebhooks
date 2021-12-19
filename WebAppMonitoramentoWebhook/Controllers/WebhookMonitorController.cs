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
    public object? Get()
    {
        _logger.LogInformation("�ltimo evento recebido: " +
            JsonSerializer.Serialize(_lastEvent,
                options: new() { WriteIndented = true }));
        _logger.LogInformation(JsonSerializer.Serialize(_lastEvent));
        return _lastEvent;
    }

    [HttpPost]
    public IActionResult Post(object data)
    {
        _lastEvent = data;
        _logger.LogInformation("Notifica��o recebida: " +
            JsonSerializer.Serialize(data,
                options: new() { WriteIndented = true }));
        return Ok();
    }
}