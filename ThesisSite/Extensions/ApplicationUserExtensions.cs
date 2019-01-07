using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Domain;
using ThesisSite.DTOs;

namespace ThesisSite.Extensions
{
    public static class ApplicationUserExtensions
    {
        public static StudentDto ToStudentDto(this ApplicationUser user)
        {
            return new StudentDto
            {
                Id = user.Id,
                Email = user.Email,
                IndexNumber = user.IndexNumber,
                LastName = user.LastName,
                FirstName = user.FirstName
            };
        }
    }
}
