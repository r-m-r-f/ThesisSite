﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisSite.Domain
{
    public class ApplicationUser : IdentityUser 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IndexNumber { get; set; }
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        //[DataType(DataType.Password)]
        //public string Password { get; set; }
    }
}
