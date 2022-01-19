using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectArchive_APi.Models.DBModels
{
    public partial class AssetsDepartmentRelation
    {
        public int AssetId { get; set; }
        public int DepartmentId { get; set; }

        public virtual Assets Asset { get; set; }
        public virtual Departments Department { get; set; }
    }
}
