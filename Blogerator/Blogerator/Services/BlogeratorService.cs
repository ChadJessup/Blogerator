using Microsoft.EntityFrameworkCore;

namespace Blogerator.Services;

public class BlogeratorService : IHostedService
{
    private readonly IOptions<BlogeratorOptions> options;
    private readonly ILogger<BlogeratorService> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly BlogeratorContainer container;

    public BlogeratorService(
        ILogger<BlogeratorService> logger,
        IOptions<BlogeratorOptions> options,
        IServiceProvider serviceProvider,
        BlogeratorContainer container)
    {
        this.logger = logger;
        this.options = options;
        this.container = container;
        this.serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken token)
    {
        using var scope = this.serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BlogeratorDbContext>();

        var blogDetails = await context.Blogs.FirstOrDefaultAsync(token);
        await context.Posts.LoadAsync(token);

        if (blogDetails is null)
        {
            // TODO: Allow bootstrapping via configuration.
            this.logger.LogError("Need at least basic Blog details to start!");
            throw new InvalidOperationException("Need at least basic Blog details to start!");
        }

        this.container.BlogTitle = blogDetails.Name;
        this.container.Posts = blogDetails.Posts;

        this.container.IsInitialized = true;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
