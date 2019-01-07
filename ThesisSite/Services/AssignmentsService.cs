using System;
using System.Collections.Generic;
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

        public Task<Assignment> GetAssignmentById(int id)
        {
            return _context.Assignments
                .SingleOrDefaultAsync(x => x.ID == id && !x.IsDeleted);
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
