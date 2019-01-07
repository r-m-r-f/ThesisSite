using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class Topic : IBaseEntity
    {
        public int ID { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
        public DateTimeOffset? DeletedTimestamp { get; set; }
        public bool IsDeleted { get; set; }
    }
}
