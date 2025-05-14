using Assignment.Repository.Data;
using Assignment.Repository.Interfaces;
using Assignment.Service.Interfaces;

namespace Assignment.Service.Implementations;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserRepository _userRepository;

    public AuthorizationService (IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> ValidatePassword(string email, string password)
    {
        User user = await _userRepository.FindUserByEmail(email);
        if(user != null)
        {
            return user.Password == password;
        }
        return false;
    }

}
