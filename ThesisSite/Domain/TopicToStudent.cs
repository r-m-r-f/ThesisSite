using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class TopicToStudent : IBaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
        public DateTimeOffset? DeletedTimestamp { get; set; }
        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Topic Topic { get; set; }

        public int TopicId { get; set; }

        public int Grade { get; set; }
    }
}
