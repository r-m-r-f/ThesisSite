using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Data;
using ThesisSite.Domain;
using ThesisSite.Services.Interface;

namespace ThesisSite.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly ApplicationDbContext _context;

        public GroupsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Group> GetStudentGroups(string studentId)
        {
            return _context.GroupEnrollments.Where(g => g.UserId == studentId).Select(g => g.Group).ToList();
        } 
    }
}
