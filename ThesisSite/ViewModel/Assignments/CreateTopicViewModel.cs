using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Domain;

namespace ThesisSite.ViewModel.Assignments
{
    public class CreateTopicViewModel
    {
        public int AssignmentId { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public int? Limit { get; set; }

        public Topic ToTopic()
        {
            return new Topic
            {
                AssignmentId = AssignmentId,
                ShortDescription = ShortDescription,
                Description = Description,
                Limit = Limit
            };
        }
    }
}
