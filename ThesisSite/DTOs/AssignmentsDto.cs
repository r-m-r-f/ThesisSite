using System;
using System.Collections.Generic;
using ThesisSite.Domain;

namespace ThesisSite.DTOs
{
    public class AssignmentsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset DueTo { get; set; }

        //public Group Group { get; set; }

        //public int UploadLimit { get; set; }

        //public IList<Topic> Topics { get; set; }
    }
}