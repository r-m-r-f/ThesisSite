using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.DTOs
{
    public class TopicDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public int AssignmentId { get; set; }

        public int? Limit { get; set; }

        public int Count { get; set; }
    }
}
