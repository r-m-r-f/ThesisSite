using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThesisSite.Data;
using ThesisSite.Domain;

namespace ThesisSite.Controllers
{
    public class GroupEnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupEnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GroupEnrollments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GroupEnrollments.Include(g => g.Group).Include(g => g.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GroupEnrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEnrollment = await _context.GroupEnrollments
                .Include(g => g.Group)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (groupEnrollment == null)
            {
                return NotFound();
            }

            return View(groupEnrollment);
        }

        // GET: GroupEnrollments/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_context.Groups, "ID", "ID");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: GroupEnrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CreatedTimestamp,DeletedTimestamp,IsDeleted,UserId,GroupID")] GroupEnrollment groupEnrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupEnrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_context.Groups, "ID", "ID", groupEnrollment.GroupID);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", groupEnrollment.UserId);
            return View(groupEnrollment);
        }

        // GET: GroupEnrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEnrollment = await _context.GroupEnrollments.FindAsync(id);
            if (groupEnrollment == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_context.Groups, "ID", "ID", groupEnrollment.GroupID);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", groupEnrollment.UserId);
            return View(groupEnrollment);
        }

        // POST: GroupEnrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedTimestamp,DeletedTimestamp,IsDeleted,UserId,GroupID")] GroupEnrollment groupEnrollment)
        {
            if (id != groupEnrollment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupEnrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupEnrollmentExists(groupEnrollment.ID))
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
            ViewData["GroupID"] = new SelectList(_context.Groups, "ID", "ID", groupEnrollment.GroupID);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", groupEnrollment.UserId);
            return View(groupEnrollment);
        }

        // GET: GroupEnrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEnrollment = await _context.GroupEnrollments
                .Include(g => g.Group)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (groupEnrollment == null)
            {
                return NotFound();
            }

            return View(groupEnrollment);
        }

        // POST: GroupEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupEnrollment = await _context.GroupEnrollments.FindAsync(id);
            _context.GroupEnrollments.Remove(groupEnrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupEnrollmentExists(int id)
        {
            return _context.GroupEnrollments.Any(e => e.ID == id);
        }
    }
}
