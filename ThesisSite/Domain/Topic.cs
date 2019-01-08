using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class Topic : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedTimestamp { get; set; }

        public DateTimeOffset? DeletedTimestamp { get; set; }

        public bool IsDeleted { get; set; }

        public int AssignmentId { get; set; }

        public Assignment Assignment { get; set; }

        public int? Limit { get; set; }

        public IList<TopicToStudent> Students { get; set; }

    }
}
