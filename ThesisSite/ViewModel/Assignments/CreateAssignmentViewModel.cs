using System;
using ThesisSite.Domain;

namespace ThesisSite.ViewModel.Assignments
{
    public class CreateAssignmentViewModel
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset DueTo { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public Assignment ToAssignment()
        {
            return new Assignment
            {
                GroupId = GroupId,
                Name = Name,
                DueTo = DueTo,
                ShortDescription = ShortDescription,
                Description = Description,
                UploadLimit = 5,
                IsActive = false
            };
        }
    }
}
