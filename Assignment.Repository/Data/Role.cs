namespace Assignment.Repository.Data;

public class Role
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime? Createdat { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public string? Updatedby { get; set; }
    public virtual ICollection<User> Users { get; } = new List<User>();
}
