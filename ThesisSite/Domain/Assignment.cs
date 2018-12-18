using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class Assignment : IBaseEntity
    {
        public int ID { get; set; }

        public DateTimeOffset CreatedTimestamp { get; set; }

        public DateTimeOffset? DeletedTimestamp { get; set; }

        public bool IsDeleted { get; set; }

        public int GroupId { get; set; }

        public DateTime DueTo { get; set; }

        public Group Group { get; set; }

        public IList<AssignmetsToStudent> AssignmetsToStudents { get; set; }
    }
}
