namespace AdviceRequestApp.Models;

public class AppSettings
{
    public ApiEndpoints ApiEndpoints { get; set; } = new();
    public LdapSettings LdapSettings { get; set; } = new();
    public ApplicationSettings ApplicationSettings { get; set; } = new();
}

public class ApiEndpoints
{
    public string ODataAreasUrl { get; set; } = string.Empty;
    public string FileUploadUrl { get; set; } = string.Empty;
    public string FileDeleteUrl { get; set; } = string.Empty;
    public string WorkflowSubmitUrl { get; set; } = string.Empty;
}

public class LdapSettings
{
    public string Server { get; set; } = string.Empty;
    public string BaseDn { get; set; } = string.Empty;
}

public class ApplicationSettings
{
    public bool SLAProcessing { get; set; } = true;
    public int MaxFileCount { get; set; } = 5;
    public double MaxTotalFileSizeMB { get; set; } = 1;
}
