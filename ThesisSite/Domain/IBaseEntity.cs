using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        DateTimeOffset CreatedTimestamp { get; set; }

        DateTimeOffset? DeletedTimestamp { get; set; }

        bool IsDeleted { get; set; }
    }
}
