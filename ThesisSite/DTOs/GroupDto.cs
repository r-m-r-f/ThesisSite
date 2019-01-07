using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.DTOs
{
    public class GroupDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? Limit { get; set; }

        public int StudentCount { get; set; }

        public bool CanEnroll => StudentCount < Limit;
    }
}
