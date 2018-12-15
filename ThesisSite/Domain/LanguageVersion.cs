using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    // TODO: Move to a separate class
    public enum Languages
    {
        Polish,
        English
    }

    public class LanguageVersion
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public Languages Language { get; set; }
          
        public Course Course { get; set; }
    }
}
