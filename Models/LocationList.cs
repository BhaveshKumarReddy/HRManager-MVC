using System;
using System.Collections.Generic;

#nullable disable

namespace HRM_Project.Models
{
    public partial class LocationList
    {
        public LocationList()
        {
            EmployeesLists = new HashSet<EmployeesList>();
            HrLists = new HashSet<HrList>();
        }

        public string LocationId { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<EmployeesList> EmployeesLists { get; set; }
        public virtual ICollection<HrList> HrLists { get; set; }
    }
}
