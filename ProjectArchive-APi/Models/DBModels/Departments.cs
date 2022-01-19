using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectArchive_APi.Models.DBModels
{
    public partial class Departments
    {
        public Departments()
        {
            AssetsDepartmentRelation = new HashSet<AssetsDepartmentRelation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AssetsDepartmentRelation> AssetsDepartmentRelation { get; set; }
    }
}
