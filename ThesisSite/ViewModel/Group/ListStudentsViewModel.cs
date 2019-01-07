using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.DTOs;

namespace ThesisSite.ViewModel.Group
{
    public class ListStudentsViewModel
    {
        public int GroupId { get; set; }

        public IEnumerable<StudentDto> Students { get; set; }
    }
}
