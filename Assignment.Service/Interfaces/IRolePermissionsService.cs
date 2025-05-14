namespace Assignment.Service.Interfaces;

public interface IRolePermissionsService
{
    Task<string> GetRoleNameByRoleId(int userRoleId);
}
