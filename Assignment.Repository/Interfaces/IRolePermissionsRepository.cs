namespace Assignment.Repository.Interfaces;

public interface IRolePermissionsRepository
{
    Task<string> FindRoleNameByRoleId(int id);
}
