using System.ComponentModel.DataAnnotations;

namespace Assignment.Repository.Data;

public class Blog
{
    public int Id { get; set; }
    [MaxLength(255)]
    public required string Title { get; set; }

    public string? Content { get; set; }
    public DateTime? Publisheddate { get; set; }
    [MaxLength(255)]
    public string? Tags { get; set; }
    public bool Isdeleted { get; set; }
}
