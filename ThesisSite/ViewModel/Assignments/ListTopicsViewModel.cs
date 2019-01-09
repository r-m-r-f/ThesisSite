using System.Collections.Generic;
using ThesisSite.DTOs;

namespace ThesisSite.ViewModel.Assignments
{
    public class ListTopicsViewModel
    {
        public int AssignmentId { get; set; }

        public IEnumerable<TopicDto> Topics { get; set; }
    }
}