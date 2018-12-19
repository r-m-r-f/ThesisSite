using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Domain;

namespace ThesisSite.ViewModel.Course
{
    public class CourseGroupsViewModel
    {
        public int CourseId { get; set; }

        public IEnumerable<ThesisSite.Domain.Group> Groups { get; set; }

        public string Name { get; set; }
    }
}
