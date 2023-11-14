using Microsoft.Extensions.Options;
using Flurl.Http;
using Obspi.Config;
using Flurl;

namespace Obspi.Services;

public enum MessagePriority
{
    Lowest = -2,
    Low = -1,
    Normal = 0,
    High = 1,
    Emergency = 2,
}

public interface INotificationService
{
    Task SendMessageAsync(string title, string message, MessagePriority priority);
}

public class PushoverService : INotificationService
{
    private readonly ILogger<PushoverService> _logger;
    private readonly PushoverOptions _options;

    public PushoverService(ILogger<PushoverService> logger, IOptions<PushoverOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public async Task SendMessageAsync(string title, string message, MessagePriority priority)
    {
        if (!_options.Enabled)
        {
            _logger.LogInformation("Pushover is disabled, but caller would have sent -> priority={Priority} title=\"{Title}\", message=\"{Message}\"",
                priority, title, message);
            return;
        }

        var url = "https://api.pushover.net/1/messages.json"
            .AppendQueryParam("token", _options.ApiToken)
            .AppendQueryParam("user", _options.UserId)
            .AppendQueryParam("priority", (int)priority)
            .AppendQueryParam("title", title)
            .AppendQueryParam("message", message);

        if (priority == MessagePriority.Emergency)
        {
            url = url
                .AppendQueryParam("retry", _options.EmergencyRetrySeconds)
                .AppendQueryParam("expire", _options.EmergencyExpireSeconds);
        }

        _logger.LogInformation("Sending Pushover message -> priority={Priority} title=\"{Title}\", message=\"{Message}\"",
            priority, title, message);

        try
        {
            await url.PostAsync();
        }
        catch (FlurlHttpException e)
        {
            _logger.LogError(e, "Exception sending pushover message: {Message}", e.Message);
            throw;
        }
    }
}
