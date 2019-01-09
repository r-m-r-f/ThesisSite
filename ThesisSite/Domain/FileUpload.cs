using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class FileUpload : IBaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
        public DateTimeOffset? DeletedTimestamp { get; set; }
        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Assignment Assignment { get; set; }

        public int AssignmentId { get; set; }

        //public int TopicId { get; set; }

        //public Topic Topic { get; set; }

        public int AssignmentToStudentId { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Path { get; set; }

        public AssignmetsToStudent AssignmetsToStudent { get; set; }
    }
}
