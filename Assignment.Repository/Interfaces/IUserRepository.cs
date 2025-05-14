using Assignment.Repository.Data;

namespace Assignment.Repository.Interfaces;

public interface IUserRepository
{
    Task<User> FindUserByEmail(string email);

}
