using Assignment.Repository.Data;

namespace Assignment.Repository.Interfaces;

public interface IUserRepository
{
    Task<bool> AddComment(Comment newComment);

    Task<User> FindUserByEmail(string email);
    Task<Blog> GetBlogById(int id);
    Task<List<Comment>> GetCommentsByBlogId(int id);

}
