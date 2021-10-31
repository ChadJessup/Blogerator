namespace Blogerator.Data;

public class BlogeratorContainer
{
    private readonly ILogger<BlogeratorContainer> logger;

    public BlogeratorContainer(
        ILogger<BlogeratorContainer> logger)
    {
        this.logger = logger;
    }

    public string BlogTitle { get; set; } = string.Empty;
    public virtual ICollection<Post>? Posts { get; set; }

    public bool IsInitialized { get; set; } = false;

    public Post GetNewestPost()
    {
        var newestPost = this.Posts?.MaxBy(p => p.CreatedAt);

        ArgumentNullException.ThrowIfNull(newestPost);

        return newestPost;
    }
}
