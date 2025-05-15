namespace Assignment.Repository.Data;

public class Comment
{
    public int Id { get; set; }
    public required string Content { get; set; } = null!;
    public DateTime? Createddate { get; set; }
    public int Blogid { get; set; }
    public int Userid { get; set; }
    public virtual Blog? Blog { get; set; }
    public virtual User? User { get; set; }
}
