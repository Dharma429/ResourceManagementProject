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

        public int SaveRole(Role role)
        {
            var dynamicRole = new
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedDate = role.Id > 0 ? role.CreatedDate: DateTime.Now,
                ModifiedDate = role.Id > 0 ? DateTime.Now : role.ModifiedDate,
                IsActive = role.IsActive
            };
            return DynamicRepository.AddOrUpdateDynamic("Sp_Roles_Save", dynamicRole);
        }
    }
}
