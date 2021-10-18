namespace Blogerator.Services;

public class BlogeratorService : IHostedService
{
    private readonly ILogger<BlogeratorService> logger;
    private readonly IOptions<BlogeratorOptions> options;
    private readonly BlogeratorContainer container;

    public BlogeratorService(
        ILogger<BlogeratorService> logger,
        IOptions<BlogeratorOptions> options,
        BlogeratorContainer container)
    {
        this.logger = logger;
        this.options = options;
        this.container = container;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
