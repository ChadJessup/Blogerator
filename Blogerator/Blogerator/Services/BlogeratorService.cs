using Microsoft.EntityFrameworkCore;

namespace Blogerator.Services;

public class BlogeratorService : IHostedService
{
    private readonly IOptions<BlogeratorOptions> options;
    private readonly ILogger<BlogeratorService> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly BlogeratorContainer container;
    private readonly TagManager tagManager;

    public BlogeratorService(
        ILogger<BlogeratorService> logger,
        IOptions<BlogeratorOptions> options,
        IServiceProvider serviceProvider,
        BlogeratorContainer container,
        TagManager tagManager)
    {
        this.logger = logger;
        this.options = options;
        this.container = container;
        this.tagManager = tagManager;
        this.serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken token)
    {
        using var scope = this.serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BlogeratorDbContext>();

        var blogDetails = await context.Blogs.FirstOrDefaultAsync(token);
        await context.Blogs
            .Include(e => e.Posts).ThenInclude(p => p.Tags)
            .LoadAsync(token);
            
        await this.tagManager.LoadTagsAsync(token);

        if (blogDetails is null)
        {
            // TODO: Allow bootstrapping via configuration.
            this.logger.LogError("Need at least basic Blog details to start!");
            throw new InvalidOperationException("Need at least basic Blog details to start!");
        }

        this.container.BlogTitle = blogDetails.Name;
        this.container.Posts = blogDetails.Posts;
        var post = this.container.Posts.First();

        foreach (var tag in this.tagManager.Tags)
        {
        //    await this.tagManager.AddPostToTag(tag, post, token);
        }

        //blogDetails.Tags.AddRange(post.Tags);
        // post.Tags.Add(new() { Name = "Games" });
        // post.Tags.Add(new() { Name = "Programming" });
        // 
        //await context.SaveChangesAsync(token);

        this.container.IsInitialized = true;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
