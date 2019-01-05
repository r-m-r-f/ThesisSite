using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThesisSite.Data;
using ThesisSite.Domain;
using ThesisSite.Domain.Helpers;
using ThesisSite.ViewModel.Course;
using ThesisSite.ViewModel.Group;

namespace ThesisSite.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Groups
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Groups.Include(c => c.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        //public async Task<IActionResult> StudentGroups(int? studentId)
        //{

        //}

        [Authorize]
        public async Task<IActionResult> CourseGroups(int? courseId)
        {
            if ( courseId == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups.Where(c => c.CourseID == courseId && !c.IsDeleted).ToListAsync();
            var course = await _context.Courses.SingleOrDefaultAsync(c => c.ID == courseId && !c.IsDeleted);

            var vm = new CourseGroupsViewModel
            {
                CourseId = courseId.Value,
                Groups = groups,
                Name = course.Name
            };

            return View(vm);
        }

        // GET: Groups/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        public async Task<IActionResult> ListStudents(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            var query = await _context.GroupEnrollments.Where(g => g.GroupId == groupId && !g.User.IsDeleted).Include(g => g.User).ToListAsync();
            var students = query.Select(x => x.User);

            return View(students);
        }

        public async Task<IActionResult> Enroll(int groupId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var courseId = _context.Groups.SingleOrDefault(g => g.ID == groupId && !g.IsDeleted).CourseID;

            var isAlreadyEnrolled = await _context.GroupEnrollments.AnyAsync(g => g.Group.CourseID == courseId && g.UserId == userId);

            if(isAlreadyEnrolled)
            {
                return RedirectToAction("Details", "Courses", new { id = courseId });
            }

            var enrollment = new GroupEnrollment
            {
                UserId = userId,
                GroupId = groupId
            };

            _context.GroupEnrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Courses", new { id = courseId });
        }

        // GET: Groups/Create
        [Authorize(Roles = ApplicationRoles.Admin)]
        public IActionResult Create(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }

            var vm = new CreateGroupViewModel
            {
                CourseId = courseId.Value
            };

            return View(vm);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> Create([Bind("Name, Limit, CourseId")] CreateGroupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var group = new Group
                {
                    Name = vm.Name,
                    CourseID = vm.CourseId,
                    Limit = vm.Limit
                };

                _context.Groups.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Courses", new { id = vm.CourseId});
            }
            return View(vm);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "ID", "Name", @group.CourseID);
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedTimestamp,DeletedTimestamp,IsDeleted,CourseID")] Group @group)
        {
            if (id != @group.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "ID", "Name", @group.CourseID);
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? groupId, int courseId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.SingleOrDefaultAsync(g => g.ID == groupId && !g.IsDeleted);


            if (group == null)
            {
                return NotFound();
            }

            group.IsDeleted = true;
            group.DeletedTimestamp = DateTimeOffset.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction("CourseGroups", "Groups", new { courseId });
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.ID == id);
        }
    }
}
