using ResourceManagement.BAL.Interfaces;
using ResourceManagement.BAL.Models;
using ResourceManagement.DAL;
using ResourceManagement.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagement.BAL.Services
{
    public class RoleManager:BaseManager, IRoleManager
    {
        public RoleManager(IDynamicRepository _dynamicRepository, IUnitOfWork unitOfWork)
        {
            DynamicRepository = _dynamicRepository;/*Single ton Pattern*/
        }

        public List<Role> GetRoles()
        {
            return DynamicRepository.All<Role>("Sp_Roles_Get", null);
        }

        public Role CheckRole(string role)
        {
            var dynamicRole = new
            {
                Name = role
            };
            return DynamicRepository.FindBy<Role>("Sp_CheckName", dynamicRole);
        }

        public int DeleteRole(int roleId)
        {
            return DynamicRepository.Delete(roleId, "Role");
        }
        public int SaveRole(Role role)
        {
            var dynamicRole = new
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                RoleDescription = role.RoleDescription,
                CreatedDate = role.RoleId > 0 ? role.CreatedDate: DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = role.IsActive
            };
            return DynamicRepository.AddOrUpdateDynamic("Sp_Roles_Save", dynamicRole);
        }
    }
}
