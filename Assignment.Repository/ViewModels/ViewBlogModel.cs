using Assignment.Repository.Data;

namespace Assignment.Repository.ViewModels;

public class ViewBlogModel
{
    public int Id { get; set; }
    public required string Title { get; set; } = null!;
    public string? Body { get; set; }
    public string? Tags { get; set; }
    public DateTime? PublishedDate { get; set; }
    public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
}
