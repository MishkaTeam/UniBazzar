namespace Framework.Observability;

public class ObservabilityOptions
{
    public const string SectionName = "Observability";

    public string ApplicationName { get; set; } = "UnnamedApplication";
    public string LokiUrl { get; set; } = string.Empty;
    public string OtlpEndpoint { get; set; } = string.Empty;
}
