using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ThesisSite.ViewModel.Assignments
{
    public class UploadSolutionViewModel
    {
        public int AssignmentId { get; set; }

        public IFormFile SolutionFile { get; set; }
    }
}
