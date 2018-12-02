using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class Assignment : BaseEntity
    {
        public int GroupId { get; set; }

        public DateTime DueTo { get; set; }

        public Group Group { get; set; }
    }
}
