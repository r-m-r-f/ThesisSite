using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.ViewModel.Group
{
    public class CreateGroupViewModel
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public int? Limit { get; set; }
    }
}
