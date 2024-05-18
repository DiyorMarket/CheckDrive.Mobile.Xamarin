using CheckDrive.DTOs.Role;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Roles
{
    public class MockRoleDataStore : IRoleDataStore
    {
        private readonly List<RoleDto> _roles;

        public MockRoleDataStore()
        {
            _roles = new List<RoleDto>
            {
               new RoleDto{Id = 1, Name = "Manager"},
               new RoleDto{Id = 2, Name = "Driver"}
            };
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            await Task.Delay(100);
            return _roles.ToList();
        }

        public async Task<RoleDto> GetRole(int id)
        {
            await Task.Delay(100);
            return _roles.FirstOrDefault(x => x.Id == id);
        }

        public async Task<RoleDto> CreateRole(RoleDto role)
        {
            await Task.Delay(100);
            role.Id = _roles.Max(r => r.Id) + 1;
            _roles.Add(role);
            return role;
        }

        public async Task<RoleDto> UpdateRole(int id, RoleDto role)
        {
            await Task.Delay(100);
            var exectingRole = _roles.FirstOrDefault(RoleDto => role.Id == id);
            if (exectingRole != null)
            {
                exectingRole.Name = role.Name;
            }
            return exectingRole;

        }

        public async Task DeleteRole(int id)
        {
            await Task.Delay(100);
            var exectingRole = _roles.FirstOrDefault(role => role.Id == id);
            if (exectingRole != null)
            {
                _roles.Remove(exectingRole);
            }

        }
    }
}
