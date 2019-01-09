using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Domain;
using ThesisSite.DTOs;

namespace ThesisSite.Extensions
{
    public static class AssignmentsExtensions
    {
        public static AssignmentsDto ToAssignmentsDto(this Assignment assignment)
        {
            return new AssignmentsDto
            {
                Id = assignment.Id,
                Description = assignment.Description,
                DueTo = assignment.DueTo,
                IsActive = assignment.IsActive,
                Name = assignment.Name,
                IsDeleted = assignment.IsDeleted,
                ShortDescription = assignment.ShortDescription
            };
        }
    }
}
