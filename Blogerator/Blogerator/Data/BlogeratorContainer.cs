namespace Blogerator.Data;

public class BlogeratorContainer
{
    private readonly ILogger<BlogeratorContainer> logger;

    public BlogeratorContainer(ILogger<BlogeratorContainer> logger)
    {
        this.logger = logger;
    }
}
