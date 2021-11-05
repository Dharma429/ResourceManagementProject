using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagement.BAL.Models
{
    public class Resource 
    {
        public int ResourceId { get; set; }
        public string Name { get; set; }
        public DateTime? DOJ { get; set; }
        public int TeamId { get; set; }
        public string TeamDescription { get; set; }
        public int Rate { get; set; }
        public Boolean IsBillable { get; set; }
        public string Manager { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Location { get; set; }
        public string EmpId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Boolean IsActive { get; set; } = true;
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
