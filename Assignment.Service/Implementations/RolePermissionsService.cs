using Assignment.Repository.Interfaces;
using Assignment.Service.Interfaces;

namespace Assignment.Service.Implementations;

public class RolePermissionsService : IRolePermissionsService
{
    private readonly IRolePermissionsRepository _rolePermissionsRepository;

    public RolePermissionsService(IRolePermissionsRepository rolePermissionsRepository)
    {
        _rolePermissionsRepository = rolePermissionsRepository;
    }

    public async Task<string> GetRoleNameByRoleId(int userRoleId)
    {
        return await _rolePermissionsRepository.FindRoleNameByRoleId(userRoleId);
    }
}
