using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class AssignmetsToStudent
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int AssignmentId { get; set; }

        public ApplicationUser User { get; set; }

        public Assignment Assignment { get; set; }

        public IList<FileUpload> Uploads { get; set; }

        public bool IsDeleted { get; set; }
    }
}
