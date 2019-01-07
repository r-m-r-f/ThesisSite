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
                .AnyAsync(x => x.Group.ID == groupId && x.UserId == userId && !x.IsDeleted);

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
            //return await _context.GroupEnrollments
            //    .Where(x => x.GroupId == groupId && !x.User.IsDeleted)
            //    .Include(x => x.User)
            //    .Select(x => x.User)
            //    .CountAsync();
        }

        public async Task<int?> GetEnrolledGroupId(string userId, int courseId)
        {
            var group = await _context.GroupEnrollments
                .SingleOrDefaultAsync(x => x.Group.CourseID == courseId && x.UserId == userId && !x.IsDeleted);

            return group?.GroupId;
        }

        public async Task Withdraw(string userId, int groupId)
        {
            var enrollment = await _context.GroupEnrollments
                .SingleOrDefaultAsync(x => !x.IsDeleted && x.UserId == userId && x.GroupId == groupId);

            enrollment.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupDto>> GetCourseGroupDtosAsync(int courseId)
        {
            var aaa = await GetCourseGroups(courseId);

            var dtos =  aaa.Select(x => new GroupDto
            {
                ID = x.ID,
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
