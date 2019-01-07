using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class FileUpload
    {
        public int Id { get; set; }

        //public string UserId { get; set; }

        public int AssignmentToStudentId { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Path { get; set; }

        public AssignmetsToStudent AssignmetsToStudent { get; set; }
    }
}
