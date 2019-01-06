using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Domain;
using ThesisSite.ViewModel.Assignments;

namespace ThesisSite.Services.Interface
{
    public interface IAssignmentsService
    {
        Task<Assignment> GetAssignmentById(int id);
        Task<IEnumerable<FileUpload>> GetUploadedSolutions(string userId, int assignmentId);
        Task UploadSolution(string userId, UploadSolutionViewModel vm);
    }
}
