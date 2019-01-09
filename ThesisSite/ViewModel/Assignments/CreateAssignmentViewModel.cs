using System;
using System.ComponentModel.DataAnnotations;
using ThesisSite.Domain;

namespace ThesisSite.ViewModel.Assignments
{
    public class CreateAssignmentViewModel
    {
        [Required]
        public int GroupId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset DueTo { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
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
