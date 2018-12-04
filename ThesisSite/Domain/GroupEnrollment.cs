using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class GroupEnrollment : BaseEntity
    {
        public int GroupID { get; set; }

        public ApplicationUser User { get; set; }

        public Group Group { get; set; }
    }
}
