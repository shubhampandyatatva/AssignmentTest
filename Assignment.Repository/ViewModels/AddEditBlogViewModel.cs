using System.ComponentModel.DataAnnotations;

namespace Assignment.Repository.ViewModels;

public class AddEditBlogViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required!")]
    public required string Title { get; set; } = null!;
    public string? Body { get; set; }
    public string? Tags { get; set; }
}
