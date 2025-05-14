using Assignment.Repository.Data;

namespace Assignment.Service.Interfaces;

public interface IJWTService
{
    public Task<string> GenerateJwtToken(User user);
    string GetClaimValue(string jwtToken, string email);

}
