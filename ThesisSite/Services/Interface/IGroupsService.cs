using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Domain;
using ThesisSite.DTOs;
using ThesisSite.ViewModel.Group;

namespace ThesisSite.Services.Interface
{
    public interface IGroupsService
    {
        Task<List<Group>> GetStudentGroups(string studentId);
        Task<bool> GroupExists(int id);
        Task<IEnumerable<Group>> GetCourseGroups(int courseId);
        Task<Group> GetGroupById(int id);
        Task<List<ApplicationUser>> GetEnrolledStudents(int groupId);
        Task CreateGroup(CreateGroupViewModel vm);
        Task Enroll(string userId, int groupId);
        Task<int> GetEnrolledStudentsCount(int groupId);
        Task<int?> GetEnrolledGroupId(string userId, int courseId);
        Task RemoveFromGroup(string userId, int groupId);
        Task<IEnumerable<GroupDto>> GetCourseGroupDtosAsync(int courseId);
        Task<IEnumerable<ApplicationUser>> GetNotEnrolledUsers(int groupId);
        Task RemoveGroup(int groupId);
    }
}
