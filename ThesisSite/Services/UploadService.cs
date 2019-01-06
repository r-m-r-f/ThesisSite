using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Data;
using ThesisSite.Services.Interface;
using ThesisSite.ViewModel.Assignments;

namespace ThesisSite.Services
{
    public class UploadService : IUploadService
    {
        private readonly ApplicationDbContext _context;

        private readonly string uploadDir = @"duppa";

        public UploadService(ApplicationDbContext context)
        {
            _context = context;
        }

        // TODO: Add summary 
        // TODO: Add file size validation
        // return path to uploaded file
        public async Task<string> UploadFile(string userId, UploadSolutionViewModel vm)
        {
            var path = $"{uploadDir}/{vm.AssignmentId}/{userId}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fs = new FileStream($"{path}/{vm.SolutionFile.FileName}", FileMode.Create))
            {
                await vm.SolutionFile.CopyToAsync(fs);
            }

            return path;
        }
    }
}
