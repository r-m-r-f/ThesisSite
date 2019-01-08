using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class Course : IBaseEntity
    {      
        public int Id { get; set; }

        public DateTimeOffset CreatedTimestamp { get; set; }

        public DateTimeOffset? DeletedTimestamp { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public Languages? Language { get; set; }

        public IList<Group> Groups { get; set; }

        public IList<LanguageVersion> LanguageVersions { get; set; }
    }
}
