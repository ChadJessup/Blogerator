namespace Blogerator.Options;

public class AzureAdOptions
{
    public string Instance { get; set; } = "https://login.microsoftonline.com/";
    public string Domain { get; set; } = "";
    public string TenantId { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string CallbackPath { get; set; } = "/signin-oidc";
}
