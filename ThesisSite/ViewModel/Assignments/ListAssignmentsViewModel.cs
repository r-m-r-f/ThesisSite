using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.DTOs;

namespace ThesisSite.ViewModel.Assignments
{
    public class ListAssignmentsViewModel
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public IEnumerable<AssignmentsDto> Assignments { get; set; }
    }
}
