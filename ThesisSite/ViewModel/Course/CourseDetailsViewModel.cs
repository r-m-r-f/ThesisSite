using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.ViewModel.Course
{
    public class CourseDetailsViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? GroupId { get; set; }

        //public static CourseDetailsViewModel FromCourse(Domain.Course course)
        //{
        //    return new CourseDetailsViewModel
        //    {
        //        ID = course.ID,
        //        GroupId = 
        //    }
        //}

    }
}
