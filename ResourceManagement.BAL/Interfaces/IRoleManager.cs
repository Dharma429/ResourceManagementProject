using ResourceManagement.BAL.Models;
using ResourceManagement.DAL;
using ResourceManagement.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagement.BAL.Interfaces
{
    public interface IRoleManager
    {
        IUnitOfWork UnitOfWork { get; set; }
        IDynamicRepository DynamicRepository { get; set; }
        List<Role> GetRoles();
        int SaveRole(Role role);
        Role CheckRole(string role);

        int DeleteRole(int roleId);
    }
}
