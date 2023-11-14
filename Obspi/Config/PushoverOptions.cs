namespace Obspi.Config;

public class PushoverOptions
{
    public const string Pushover = "Pushover";

    public bool Enabled { get; set; } = false;
    public string ApiToken { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int EmergencyRetrySeconds { get; set; } = 60;
    public int EmergencyExpireSeconds { get; set; } = 7200;
}
