using Assignment.Repository.Data;
using Assignment.Repository.ViewModels;

namespace Assignment.Service.Interfaces;

public interface IUserService
{
    Task<bool> AddComment(string comment, int blogId, string userId);

    Task<ViewBlogModel> GetAddEditBlogViewModel(int id);
    Task<List<CommentViewModel>> GetCommentViewModelByBlogId(int id);
    Task<User> GetUserByEmail(string email);

}
