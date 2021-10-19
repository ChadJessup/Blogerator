namespace Blogerator.Data;

public class Blog
{
    public long Id { get; set; }
    public string Name { get; set; } = "Blogerator";

    public List<Post> Posts { get; set; } = new();
}

public record Post
{
    public long Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string TitleImageUrl { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
}
