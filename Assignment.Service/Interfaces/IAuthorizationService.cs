
namespace Assignment.Service.Interfaces;

public interface IAuthorizationService
{
    Task<bool> ValidatePassword(string email, string password);
}
