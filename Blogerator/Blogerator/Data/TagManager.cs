using Microsoft.EntityFrameworkCore;

namespace Blogerator.Data;

public class TagManager
{
    private readonly ILogger<TagManager> logger;
    private readonly IServiceProvider serviceProvider;

    public TagManager(
        ILogger<TagManager> logger,
        IServiceProvider serviceProvider)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
    }

    public List<Tag> Tags { get; set; } = new List<Tag>();

    public async Task LoadTagsAsync(CancellationToken token = default)
    {
        using var scope = this.serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BlogeratorDbContext>();

        this.Tags = await context.Tags
            .Include(e => e.Posts)
            .ToListAsync(token);
    }

    public async Task<Tag> AddPostToTag(Tag tag, Post post, CancellationToken token = default)
    {
        using var scope = this.serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BlogeratorDbContext>();

        context.Tags.Attach(tag);
        context.Posts.Attach(post);

        if (!tag.Posts.Contains(post))
        {
            tag.Posts.Add(post);

            await context.SaveChangesAsync(token);
        }

        return tag;
    }
}
