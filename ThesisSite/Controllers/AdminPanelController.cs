using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisSite.Domain.Helpers;

namespace ThesisSite.Controllers
{
    public class AdminPanelController : Controller
    {
        [Authorize(Roles = ApplicationRoles.Admin)]
        public IActionResult Index()
        {
            return View();
        }
    }
}