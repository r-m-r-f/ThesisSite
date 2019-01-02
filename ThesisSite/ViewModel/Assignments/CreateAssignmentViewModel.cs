using System;

namespace ThesisSite.ViewModel.Assignments
{
    public class CreateAssignmentViewModel
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset DueTo { get; set; }

        public string Description { get; set; }
    }
}
