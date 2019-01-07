using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisSite.Domain;

namespace ThesisSite.DTOs
{
    public class StudentDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IndexNumber { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }
    }
}
