using Assignment.Repository.Data;

namespace Assignment.Service.Interfaces;

public interface IUserService
{
    Task<User> GetUserByEmail(string email);

}
