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
        //Task<Assignment> GetAssignmentById(int id);
        //Task<IEnumerable<FileUpload>> GetUploadedSolutions(string userId, int assignmentId);
        //Task UploadSolution(string userId, UploadSolutionViewModel vm);
        Task<Assignment> GetAssignmentById(int assignmentId);
        Task<IEnumerable<Assignment>> GetAssignmentsFromGroup(int groupId);
        Task CreateAssignment(CreateAssignmentViewModel vm);
        Task CreateTopic(CreateTopicViewModel vm);
        Task DeleteTopic(int topicId);
        Task ActivateAssignment(int assignmentId);
        Task DectivateAssignment(int assignmentId);
        Task DeleteAssignment(int assignmentId);
        void AddStudentsToTopic(int topicId, IEnumerable<string> studentIds);
        Task<IEnumerable<Topic>> GetTopicsByAssignmentId(int assignmentId);
        Task<int> CountAssignedToTopic(int topicId);
        Task AddStudentToTopic(int topicId, string studentId);
        Task<int?> IsStudentAssignedToTopic(int assignmentId, string studentId);
        Task UploadSolution(string userId, UploadSolutionViewModel vm);
    }
}
