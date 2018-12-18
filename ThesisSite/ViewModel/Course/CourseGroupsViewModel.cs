using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Domain;

namespace ThesisSite.ViewModel.Course
{
    public class CourseGroupsViewModel
    {
        public IEnumerable<Group> Groups { get; set; }

        public string Name { get; set; }
    }
}
