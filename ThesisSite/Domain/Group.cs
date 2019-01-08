using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class Group : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedTimestamp { get; set; }

        public DateTimeOffset? DeletedTimestamp { get; set; }

        public bool IsDeleted { get; set; }

        public int CourseID { get; set; }

        public Course Course { get; set; }

        public int? Limit { get; set; }

        public int AssignmentId { get; set; }
        public IList<Assignment> Assignments { get; set; }

        public IList<GroupEnrollment> GroupEnrollments { get; set; }
    }
}
