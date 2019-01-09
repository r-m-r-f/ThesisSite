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
            var topic = vm.ToTopic();
            _context.Add(topic);
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

        public async Task<int?> IsStudentAssignedToTopic(int assignmentId, string studentId)
        {
            var tts = await _context.TopicToStudents.SingleOrDefaultAsync(x =>
                x.Topic.Assignment.Id == assignmentId && x.UserId == studentId && !x.IsDeleted);

            return tts?.TopicId;
        }

        public async void AddStudentToTopic(int topicId, string studentId)
        {
            var topicToStudent = new TopicToStudent
            {
                TopicId = topicId,
                UserId = studentId
            };

            _context.Add(topicToStudent);
            await _context.SaveChangesAsync();
        }

        public async Task ProlongAssignment(int assignmentId, DateTimeOffset newDate)
        {
            var assignment = await GetAssignmentById(assignmentId);

            if (newDate < assignment.DueTo)
            {
                throw new InvalidAssignmentDueDate();
            }

            assignment.DueTo = newDate;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Topic>> GetTopicsByAssignmentId(int assignmentId)
        {
            return await _context.Topics
                .Where(x => x.AssignmentId == assignmentId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<int> CountAssignedToTopic(int topicId)
        {
            var count = await _context.TopicToStudents
                .Where(x => x.TopicId == topicId && !x.IsDeleted)
                .CountAsync();

            return count;
        }

        public async Task<Topic> GetTopicById(int topicId)
        {
            return await _context.Topics.SingleOrDefaultAsync(x => x.Id == topicId && !x.IsDeleted);
        }


        //public async Task<IEnumerable<FileUpload>> GetUploadedSolutions(string userId, int assignmentId)
        //{
        //    return await _context.FileUploads
        //        .Where(x => x.UserId == userId && x.AssignmentId == assignmentId)
        //        .OrderBy(x => x.Timestamp)
        //        .ToListAsync();
        //}

        public async Task UploadSolution(string userId, UploadSolutionViewModel vm)
        {
            var now = DateTimeOffset.Now;
            var topic = await GetTopicById(vm.TopicId);
            var assignment = await GetAssignmentById(topic.AssignmentId);
            var count = await GetUploadedSolutionsCount(userId, vm.TopicId);

            if (assignment.UploadLimit <= count)
            {
                throw new ExcessiveFileUploadException();
            }

            var path = await _uploadService.UploadFile(userId, vm);

            var fileUpload = new FileUpload
            {
                AssignmentId = assignment.Id,
                UserId = userId,
                Timestamp = now,
                Path = path
            };

            _context.Add(fileUpload);

            var tts = await _context.TopicToStudents
                .SingleOrDefaultAsync(x => x.TopicId == topic.Id && x.UserId == userId && !x.IsDeleted);

            tts.FileUpload = fileUpload;

            await _context.SaveChangesAsync();
        }

        private async Task<int> GetUploadedSolutionsCount(string userId, int assignmentId)
        {
            return await _context.FileUploads
                .Where(x => x.UserId == userId && x.AssignmentId == assignmentId)
                .CountAsync();
        }

    }
}
