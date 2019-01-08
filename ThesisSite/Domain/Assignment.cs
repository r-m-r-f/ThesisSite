using System;
using System.Collections.Generic;

namespace ThesisSite.Domain
{
    public class Assignment : IBaseEntity
    {
        public int Id { get; set; }

        public DateTimeOffset CreatedTimestamp { get; set; }

        public DateTimeOffset? DeletedTimestamp { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public bool IsDeleted { get; set; }

        public int GroupId { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset DueTo { get; set; }

        public Group Group { get; set; }

        public int UploadLimit { get; set; }

        public IList<Topic> Topics { get; set; }

        //public IList<AssignmetsToStudent> AssignmetsToStudents { get; set; }
    }
}
