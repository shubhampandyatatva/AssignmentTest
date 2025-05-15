namespace Assignment.Repository.ViewModels;

public class BlogListViewModel
{
    public int Id { get; set; }
    public required string BlogTitle { get; set; }
    public string? BlogContent { get; set; }
    public DateTime? PublishedDate { get; set; }
}
