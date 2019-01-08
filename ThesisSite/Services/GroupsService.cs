using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ThesisSite.Data;
using ThesisSite.Domain;
using ThesisSite.DTOs;
using ThesisSite.Services.Interface;
using ThesisSite.ViewModel.Course;
using ThesisSite.ViewModel.Group;

namespace ThesisSite.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAssignmentsService _assignmentsService;

        public GroupsService(ApplicationDbContext context, IAssignmentsService assignmentsService)
        {
            _context = context;
            _assignmentsService = assignmentsService;
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
            return _context.Groups.AnyAsync(e => e.Id == id);
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
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public Task<List<ApplicationUser>> GetEnrolledStudents(int groupId)
        {
            return _context.GroupEnrollments
                .Where(x => x.GroupId == groupId && !x.Group.IsDeleted && !x.User.IsDeleted && !x.IsDeleted)
                .Include(x => x.User)
                .Select(x => x.User)
                .ToListAsync();
        }

        // TODO: Refactor
        public async Task<IEnumerable<ApplicationUser>> GetNotEnrolledUsers(int groupId)
        {
            var students = await _context.Users.Where(x => !x.IsDeleted).ToListAsync();

            return students;
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
                .AnyAsync(x => x.Group.Id == groupId && x.UserId == userId && !x.IsDeleted);

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
            var students = await GetEnrolledStudents(groupId);

            return students.Count();
        }

        public async Task<int?> GetEnrolledGroupId(string userId, int courseId)
        {
            var group = await _context.GroupEnrollments
                .SingleOrDefaultAsync(x => x.Group.CourseID == courseId && x.UserId == userId && !x.IsDeleted);

            return group?.GroupId;
        }

        public async Task RemoveFromGroup(string userId, int groupId)
        {
            var enrollment = await _context.GroupEnrollments
                .SingleOrDefaultAsync(x => !x.IsDeleted && x.UserId == userId && x.GroupId == groupId);

            enrollment.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveGroup(int groupId)
        {
            var group = await _context.Groups.SingleOrDefaultAsync(x => x.Id == groupId && !x.IsDeleted);

            if (group != null)
            {
                var assignments = group.Assignments?.ToImmutableList();

                if (assignments != null)
                {
                    foreach (var assignment in assignments)
                    {
                        await _assignmentsService.DeleteAssignment(assignment.Id);
                    }
                }

                var enrollments = _context.GroupEnrollments.Where(x => x.GroupId == groupId && !x.IsDeleted).ToImmutableList();

                if (enrollments != null)
                {
                    _context.RemoveRange(enrollments);
                }

                _context.Remove(group);

                await _context.SaveChangesAsync();
            }
        } 

        public async Task<IEnumerable<GroupDto>> GetCourseGroupDtosAsync(int courseId)
        {
            var groups = await GetCourseGroups(courseId);

            var dtos =  groups.Select(x => new GroupDto
            {
                ID = x.Id,
                Limit = x.Limit,
                Name = x.Name
            }).ToImmutableList();

            foreach (var dto in dtos)
            {
                dto.StudentCount = await GetEnrolledStudentsCount(dto.ID);
            }

            return dtos;
        }
    }
}
