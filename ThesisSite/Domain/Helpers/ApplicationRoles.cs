using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain.Helpers
{
    public static class ApplicationRoles
    {
        public const string Admin = "Admin";

        public const string Student = "Student";

        public static IEnumerable<string> Roles = new[] { Admin, Student };
    }
}
