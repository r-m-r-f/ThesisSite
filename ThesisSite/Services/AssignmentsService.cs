using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThesisSite.Data;
using ThesisSite.Domain;
using ThesisSite.Exceptions;
using ThesisSite.Services.Interface;
using ThesisSite.ViewModel.Assignments;

namespace ThesisSite.Services
{
    public class AssignmentsService : IAssignmentsService
    {
        private readonly ApplicationDbContext _context;

        private readonly IUploadService _uploadService;

        public AssignmentsService(ApplicationDbContext context, IUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        public Task<Assignment> GetAssignmentById(int assignmentId)
        {
            return _context.Assignments
                .SingleOrDefaultAsync(x => x.Id == assignmentId && !x.IsDeleted);
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsFromGroup(int groupId)
        {
            return await _context.Assignments
                .Where(x => !x.IsDeleted && x.GroupId == groupId)
                .ToListAsync();
        }

        public async Task CreateAssignment(CreateAssignmentViewModel vm)
        {
            _context.Add(vm.ToAssignment());
            await _context.SaveChangesAsync();
        }

        public async Task CreateTopic(CreateTopicViewModel vm)
        {
            _context.Add(vm.ToTopic());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTopic(int topicId)
        {
            var topic = await _context.Topics.SingleOrDefaultAsync(x => x.Id == topicId && !x.IsDeleted);

            if (topic != null)
            {
                topic.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActivateAssignment(int assignmentId)
        {
            var assignment = await _context.Assignments.SingleOrDefaultAsync(x => x.Id == assignmentId && !x.IsDeleted);

            if (assignment != null)
            {
                assignment.IsActive = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DectivateAssignment(int assignmentId)
        {
            var assignment = await _context.Assignments.SingleOrDefaultAsync(x => x.Id == assignmentId && !x.IsDeleted);

            if (assignment != null)
            {
                assignment.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAssignment(int assignmentId)
        {
            var assignment = await _context.Assignments.SingleOrDefaultAsync(x => x.Id == assignmentId && !x.IsDeleted);

            if (assignment != null)
            {
                var topics = assignment.Topics?.ToImmutableList();

                if (topics != null)
                {
                    foreach (var topic in topics)
                    {
                        _context.Remove(topic);
                    }
                }

                _context.Remove(assignment);
                await _context.SaveChangesAsync();
            }
        }

        public async void AddStudentsToTopic(int topicId, IEnumerable<string> studentIds)
        {
            foreach (var studentId in studentIds)
            {
                var topicToStudent = new TopicToStudent
                {
                    TopicId = topicId,
                    UserId = studentId
                };

                _context.Add(topicToStudent);
            }

            await _context.SaveChangesAsync();
        }


        //public async Task<IEnumerable<FileUpload>> GetUploadedSolutions(string userId, int assignmentId)
        //{
        //    return await _context.FileUploads
        //        .Where(x => x.UserId == userId && x.AssignmentId == assignmentId)
        //        .OrderBy(x => x.Timestamp)
        //        .ToListAsync();
        //}

        //public async Task UploadSolution(string userId, UploadSolutionViewModel vm)
        //{
        //    var now = DateTimeOffset.Now;
        //    var assignment = await GetAssignmentById(vm.AssignmentId);
        //    var count = await GetUploadedSolutionsCount(userId, vm.AssignmentId);

        //    if (assignment.UploadLimit <= count)
        //    {
        //        throw new ExcessiveFileUploadException();
        //    }

        //    var path = await _uploadService.UploadFile(userId, vm);

        //    var fileUpload = new FileUpload
        //    {
        //        AssignmentId = vm.AssignmentId,
        //        UserId = userId,
        //        Timestamp = now,
        //        Path = path
        //    };

        //    _context.Add(fileUpload);
        //    await _context.SaveChangesAsync();
        //}

        //private async Task<int> GetUploadedSolutionsCount(string userId, int assignmentId)
        //{
        //    return await _context.FileUploads
        //        .Where(x => x.UserId == userId && x.AssignmentId == assignmentId)
        //        .CountAsync();
        //}

    }
}
