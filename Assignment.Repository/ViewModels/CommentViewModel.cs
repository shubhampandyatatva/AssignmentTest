using System.ComponentModel.DataAnnotations;

namespace Assignment.Repository.ViewModels;

public class CommentViewModel
{
    public int Id { get; set; }
    
    [MaxLength(255)]
    [Required(ErrorMessage = "Please write something in the comment box")]
    public required string Content { get; set; } = null!;
    public DateTime? Createddate { get; set; }
    public int Blogid { get; set; }
    public int Userid { get; set; }
    public string? UserName { get; set; }
}
