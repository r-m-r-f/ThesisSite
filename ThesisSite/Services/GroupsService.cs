using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThesisSite.Data;
using ThesisSite.Domain;
using ThesisSite.Services.Interface;
using ThesisSite.ViewModel.Course;
using ThesisSite.ViewModel.Group;

namespace ThesisSite.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly ApplicationDbContext _context;

        public GroupsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Group>> GetStudentGroups(string studentId)
        {
            return _context.GroupEnrollments
                .Where(g => g.UserId == studentId && !g.IsDeleted)
                .Select(g => g.Group)
                .ToListAsync();
        }

        public Task<bool> GroupExists(int id)
        {
            return _context.Groups.AnyAsync(e => e.ID == id);
        }

        // TODO: CREATE DTO!!!!
        public async Task<IEnumerable<Group>> GetCourseGroups(int courseId)
        {
            return await _context.Groups
                .Where(c => c.CourseID == courseId && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _context.Groups
                .SingleOrDefaultAsync(x => x.ID == id && !x.IsDeleted);
        }

        public Task<List<ApplicationUser>> GetEnrolledStudents(int groupId)
        {
            return _context.GroupEnrollments
                .Where(x => x.GroupId == groupId && !x.User.IsDeleted)
                .Include(x => x.User)
                .Select(x => x.User)
                .ToListAsync();
        }

        public async Task CreateGroup(CreateGroupViewModel vm)
        {
            var group = new Group
            {
                Name = vm.Name,
                CourseID = vm.CourseId,
                Limit = vm.Limit
            };

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
        }

        // TODO: Use transaction
        public async Task Enroll(string userId, int groupId)
        {
            var isEnrolled = await _context.GroupEnrollments
                .AnyAsync(x => x.Group.ID == groupId && x.UserId == userId);

            if (!isEnrolled)
            {
                var enrollment = new GroupEnrollment
                {
                    UserId = userId,
                    GroupId = groupId
                };

                _context.GroupEnrollments.Add(enrollment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetEnrolledStudentsCount(int groupId)
        {
            return await _context.GroupEnrollments
                .Where(x => x.GroupId == groupId && !x.User.IsDeleted)
                .Include(x => x.User)
                .Select(x => x.User)
                .CountAsync();
        }
    }
}
