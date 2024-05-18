using CheckDrive.DTOs.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Roles
{
    public interface IRoleDataStore
    {
        Task<List<RoleDto>> GetRoles();
        Task<RoleDto> GetRole(int id);
        Task<RoleDto> CreateRole(RoleDto role);
        Task<RoleDto> UpdateRole(int id, RoleDto role);
        Task DeleteRole(int id);
    }
}
