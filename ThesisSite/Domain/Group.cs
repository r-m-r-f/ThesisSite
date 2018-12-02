using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class Group : BaseEntity
    {
        public int CourseID { get; set; }

        public Course Course { get; set; }

        public IList<GroupEnrollment> Enrollments { get; set; }
    }
}
