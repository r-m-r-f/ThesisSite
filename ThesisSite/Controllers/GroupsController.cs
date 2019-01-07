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
using ThesisSite.Extensions;
using ThesisSite.Services;
using ThesisSite.Services.Interface;
using ThesisSite.ViewModel.Course;
using ThesisSite.ViewModel.Group;

namespace ThesisSite.Controllers
{
    public class GroupsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IGroupsService _groupsService;
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupsController(/*ApplicationDbContext context,*/ IGroupsService groupsService, ICourseService courseService, UserManager<ApplicationUser> userManager)
        {
            //_context = context;
            _userManager = userManager;
            _groupsService = groupsService;
            _courseService = courseService;
        }

        // GET: Groups
        //[Authorize]
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Groups.Include(c => c.Course);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        [Authorize]
        public async Task<IActionResult> CourseGroups(int courseId)
        {
            //var groups = await _groupsService.GetCourseGroups(courseId);
            var groups = await _groupsService.GetCourseGroupDtosAsync(courseId);
            var course = await _courseService.GetCourseById(courseId);

            var vm = new CourseGroupsViewModel
            {
                CourseId = courseId,
                Groups = groups,
                Name = course.Name
            };

            return View(vm);
        }

        // GET: Groups/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var group = await _groupsService.GetGroupById(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        public async Task<IActionResult> ListStudents(int groupId)
        {
            var students = await _groupsService.GetEnrolledStudents(groupId);

            var vm = new ListStudentsViewModel
            {
                GroupId = groupId,
                Students = students.Select(x => x.ToStudentDto())
            };

            return View(vm);
        }

        public async Task<IActionResult> Enroll(int groupId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await _groupsService.Enroll(userId, groupId);

            var group = await _groupsService.GetGroupById(groupId);

            //var courseId = _context.Groups.SingleOrDefault(g => g.ID == groupId && !g.IsDeleted).CourseID;

            //var isEnrolled = await _context.GroupEnrollments.AnyAsync(g => g.Group.CourseID == courseId && g.UserId == studentId);

            //if(isEnrolled)
            //{
            //    return RedirectToAction("Details", "Courses", new { id = courseId });
            //}

            //var enrollment = new GroupEnrollment
            //{
            //    UserId = studentId,
            //    GroupId = groupId
            //};

            //_context.GroupEnrollments.Add(enrollment);
            //await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Courses", new { id = group.CourseID });
        }

        public async Task<IActionResult> AddStudent(int groupId)
        {
            var students = await _groupsService.GetNotEnrolledUsers(groupId);

            var vm = new ListStudentsViewModel
            {
                GroupId = groupId,
                Students = students.Select(x => x.ToStudentDto())
            };

            return View(vm);
        }

        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> AddStudentToGroup(string studentId, int groupId)
        {
            await _groupsService.Enroll(studentId, groupId);
            return RedirectToAction("AddStudent", new { studentId, groupId });
        }

        // GET: Groups/Create
        [Authorize(Roles = ApplicationRoles.Admin)]
        public IActionResult Create(int courseId)
        {
            var vm = new CreateGroupViewModel
            {
                CourseId = courseId
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
                await _groupsService.CreateGroup(vm);
                return RedirectToAction("Details", "Courses", new { id = vm.CourseId});
            }
            return View(vm);
        }

        //TODO: Change to POST
        public async Task<IActionResult> RemoveFromGroup(string studentId, int groupId)
        {
            await _groupsService.Withdraw(studentId, groupId);
            return RedirectToAction("ListStudents", new {groupId});
        }

        //// GET: Groups/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var @group = await _context.Groups.FindAsync(id);
        //    if (@group == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CourseID"] = new SelectList(_context.Courses, "ID", "Name", @group.CourseID);
        //    return View(@group);
        //}

        //// POST: Groups/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedTimestamp,DeletedTimestamp,IsDeleted,CourseID")] Group @group)
        //{
        //    if (id != @group.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(@group);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GroupExists(@group.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CourseID"] = new SelectList(_context.Courses, "ID", "Name", @group.CourseID);
        //    return View(@group);
        //}

        //// GET: Groups/Delete/5
        //public async Task<IActionResult> Delete(int? groupId, int courseId)
        //{
        //    if (groupId == null)
        //    {
        //        return NotFound();
        //    }

        //    var group = await _context.Groups.SingleOrDefaultAsync(g => g.ID == groupId && !g.IsDeleted);


        //    if (group == null)
        //    {
        //        return NotFound();
        //    }

        //    group.IsDeleted = true;
        //    group.DeletedTimestamp = DateTimeOffset.Now;
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("CourseGroups", "Groups", new { courseId });
        //}

        //// POST: Groups/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var @group = await _context.Groups.FindAsync(id);
        //    _context.Groups.Remove(@group);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool GroupExists(int id)
        //{
        //    return _context.Groups.Any(e => e.ID == id);
        //}
    }
}
