namespace Server.Infrastructure.Settings;

public class ApplicationSettings : object
{
    public static readonly string KeyName = nameof(ApplicationSettings);

    public ApplicationSettings() : base()
    {
        ToastSettings =
            new ToastSettings();
    }

    // **********
    public string? Version { get; set; }
    // **********

    // **********
    public string? MasterPassword { get; set; }
    // **********

    // **********
    public string[]? ActivationKeys { get; set; }
    // **********

    // **********
    public ToastSettings ToastSettings { get; set; }
    // **********
}
