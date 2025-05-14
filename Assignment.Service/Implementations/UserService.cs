using Assignment.Repository.Data;
using Assignment.Repository.Interfaces;
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
}
