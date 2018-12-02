using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class BaseEntity
    {
        public int ID { get; set; }

        public DateTime CreatedTimestamp { get; set; }

        public DateTime DeletedTimestamp { get; set; }

        public bool IsDeleted { get; set; }
    }
}
