namespace Blogerator.Options;

public class BlogeratorOptions
{
    public bool DetailedErrors { get; set; } = false;
    public AzureAdOptions AzureAd { get; set; } = new();
    public ConnectionStrings ConnectionStrings { get; set; } = new();
}
