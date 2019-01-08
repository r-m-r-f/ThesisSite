using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThesisSite.Domain;
using ThesisSite.Domain.Helpers;
using ThesisSite.Services.Interface;
using ThesisSite.ViewModel.Course;

namespace ThesisSite.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IGroupsService _groupsService;

        public CoursesController(ICourseService courseService, IGroupsService groupsService)
        {
            _groupsService = groupsService;
            _courseService = courseService;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var data = await _courseService.GetAll();
            return View(data);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            var vm = new CourseDetailsViewModel
            {
                Description = course.Description,
                ID = course.Id,
                Name = course.Name
            };

            if (User.IsInRole(ApplicationRoles.Student))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                vm.GroupId = await _groupsService.GetEnrolledGroupId(userId, course.Id);
            }

            return View(vm);
        }

        // GET: Courses/Create
        [Authorize(Roles = ApplicationRoles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> Create([Bind("Name,ShortDescription,Description,Language")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseService.AddCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ID,CreatedTimestamp,DeletedTimestamp,IsDeleted")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseService.UpdateCourse(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                    //if (!CourseExists(course.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteCourse(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
