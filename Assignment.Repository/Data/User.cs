namespace Assignment.Repository.Data;

public class User
{
    public int Id { get; set; }
    public required string Firstname { get; set; }
    public string? Lastname { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required int Roleid { get; set; }
    public required string Phone { get; set; }
    public string? Profileimage { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public bool? Isdeleted { get; set; }
    public int? Createdby { get; set; }
    public DateTime? Createdat { get; set; }
    public int? Updatedby { get; set; }
    public DateTime? Updatedat { get; set; }
    public virtual Role? Role { get; set; }
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
}
