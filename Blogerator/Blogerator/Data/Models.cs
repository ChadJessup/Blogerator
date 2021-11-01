namespace Blogerator.Data;

public class Blog
{
    public long Id { get; set; }
    public string Name { get; set; } = "Blogerator";
    public IList<Post> Posts { get; } = new List<Post>();
}

public record Post
{
    public long Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string TitleImageUrl { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public IList<Tag> Tags { get; } = new List<Tag>();

    public long BlogId { get; set; }
    public Blog Blog { get; set; }
}

public record Tag
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Post> Posts { get; } = new List<Post>();
}
