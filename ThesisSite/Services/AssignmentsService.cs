using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Data;
using ThesisSite.Services.Interface;

namespace ThesisSite.Services
{
    public class AssignmentsService : IAssignmentsService
    {
        private readonly ApplicationDbContext _context;

        public AssignmentsService(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
