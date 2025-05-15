using Assignment.Repository.Data;
using Assignment.Repository.Interfaces;
using Assignment.Repository.ViewModels;
using Assignment.Service.Interfaces;

namespace Assignment.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userRepository.FindUserByEmail(email);
    }    

    public async Task<ViewBlogModel> GetAddEditBlogViewModel(int id)
    {
        Blog blog = await _userRepository.GetBlogById(id);
        if (blog == null)
        {
            return null;
        }

        List<Comment> comments = await _userRepository.GetCommentsByBlogId(id);
        List<CommentViewModel> commentViewModels = comments.Select(c => new CommentViewModel
        {
            Id = c.Id,
            Content = c.Content,
            Createddate = c.Createddate,
            Userid = c.Userid,
            Blogid = c.Blogid,
            UserName = c.User?.Firstname + " " + c.User?.Lastname
        }).ToList();

        return new ViewBlogModel
        {
            Id = blog.Id,
            Title = blog.Title,
            Body = blog.Content,
            Tags = blog.Tags,
            Comments = commentViewModels,
        };
    }

    public Task<bool> AddComment(string comment, int blogId, string userId)
    {
        Comment newComment = new()
        {
            Content = comment,
            Blogid = blogId,
            Userid = int.Parse(userId),
            Createddate = DateTime.UtcNow,
        };

        return _userRepository.AddComment(newComment);
    }

    public async Task<List<CommentViewModel>> GetCommentViewModelByBlogId(int id)
    {
        List<Comment> comments = await _userRepository.GetCommentsByBlogId(id);
        List<CommentViewModel> commentViewModels = comments.Select(c => new CommentViewModel
        {
            Id = c.Id,
            Content = c.Content,
            Createddate = c.Createddate,
            Userid = c.Userid,
            Blogid = c.Blogid,
            UserName = c.User?.Firstname + " " + c.User?.Lastname
        }).ToList();

        return commentViewModels;
    }
}
