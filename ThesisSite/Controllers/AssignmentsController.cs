using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ThesisSite.Data;
using ThesisSite.Domain;
using ThesisSite.DTOs;
using ThesisSite.Extensions;
using ThesisSite.Services.Interface;
using ThesisSite.ViewModel.Assignments;

namespace ThesisSite.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAssignmentsService _assignmentsService;
        private readonly IGroupsService _groupsService;


        public AssignmentsController(ApplicationDbContext context, IAssignmentsService assignmentsService, IGroupsService groupsService, UserManager<ApplicationUser> userManager)
        {
            _groupsService = groupsService;
            _context = context;
            _userManager = userManager;
            _assignmentsService = assignmentsService;
        }

        // GET: Assignments
        public async Task<IActionResult> ListAssignments(int groupId)
        {
            //var applicationDbContext = _context.Assignments.Include(a => a.Group);
            //return View(await applicationDbContext.ToListAsync());

            var group = await _groupsService.GetGroupById(groupId);
            var assignments = await _assignmentsService.GetAssignmentsFromGroup(groupId);

            var vm = new ListAssignmentsViewModel
            {
                GroupId = groupId,
                Name = group.Name,
                Assignments = assignments.Select(x => x.ToAssignmentsDto())
            };

            return View(vm);
        }

        public async Task<IActionResult> UploadSolution(int topicId)
        {
            var vm = new UploadSolutionViewModel
            {
                TopicId = topicId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadSolution(UploadSolutionViewModel vm)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var topic = _assignmentsService.ge

            await _assignmentsService.UploadSolution(userId, vm);

            return RedirectToAction("Index", "Courses");
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult CreateAssignment(int groupId)
        {
            var vm = new CreateAssignmentViewModel
            {
                GroupId = groupId,
                DueTo = DateTimeOffset.Now.AddDays(14)
            };

            return View(vm);
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAssignment([Bind("GroupId,DueTo,Name, ShortDescription, Description")] CreateAssignmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var assignment = new Assignment
                {
                    GroupId = vm.GroupId,
                    DueTo = vm.DueTo.DateTime,
                    Name = vm.Name,
                    Description = vm.Description
                };

                await _assignmentsService.CreateAssignment(vm);
                return RedirectToAction("ListAssignments", new { groupId = vm.GroupId });
            }
            return RedirectToAction("ListAssignments", new { groupId = vm.GroupId });
        }

        public async Task<IActionResult> DeleteAssignment(int assignmentId, int groupId)
        {
            await _assignmentsService.DeleteAssignment(assignmentId);
            return RedirectToAction("ListAssignments", new { groupId = groupId });
        }

        public async Task<IActionResult> DeactivateAssignment(int assignmentId, int groupId)
        {
            await _assignmentsService.DectivateAssignment(assignmentId);
            return RedirectToAction("ListAssignments", new { groupId = groupId });
        }

        public async Task<IActionResult> ActivateAssignment(int assignmentId, int groupId)
        {
            await _assignmentsService.ActivateAssignment(assignmentId);
            return RedirectToAction("ListAssignments", new { groupId = groupId });
        }

        [Authorize]
        public async Task<IActionResult> ActiveAssignments(int groupId)
        {
            var now = DateTimeOffset.Now;

            var assignments = await _context.Assignments.Where(x => !x.IsDeleted && x.GroupId == groupId && x.DueTo > now).ToListAsync();
            return null;
        }

        public async Task<IActionResult> ListTopics(int assignmentId)
        {
            var topics = await _assignmentsService.GetTopicsByAssignmentId(assignmentId);

            var vm = new ListTopicsViewModel
            {
                AssignmentId = assignmentId,
                Topics = topics.Select(x => new TopicDto
                {
                    AssignmentId = x.AssignmentId,
                    Limit = x.Limit,
                    Description = x.Description,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Id = x.Id
                })
            };

            foreach (var topic in vm.Topics)
            {
                topic.Count = await _assignmentsService.CountAssignedToTopic(topic.Id);
            }

            return View(vm);
        }

        public async Task<IActionResult> GetStudentTopic(int assignmentId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var topicId = await _assignmentsService.IsStudentAssignedToTopic(assignmentId, userId);

            return topicId.HasValue ? RedirectToAction("UploadSolution", new {topicId = topicId.Value}) : RedirectToAction("ListTopics", new {assignmentId});
        }

    public async Task<IActionResult> DeleteTopic(int topicId, int assignmentId)
        {
            await _assignmentsService.DeleteTopic(topicId);
            return RedirectToAction("ListTopics", new {assignmentId});
        }

        public async Task<IActionResult> CreateTopic(int assignmentId)
        {
            var vm = new CreateTopicViewModel
            {
                AssignmentId = assignmentId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTopic([Bind] CreateTopicViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _assignmentsService.CreateTopic(vm);
                return RedirectToAction("ListTopics", new { assignmentId = vm.AssignmentId });
            }

            return RedirectToAction("Index", "Courses");
        }


        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,DueTo,ID,CreatedTimestamp,DeletedTimestamp,IsDeleted")] Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListAssignments));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "ID", "ID", assignment.GroupId);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListAssignments));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }
    }
}
