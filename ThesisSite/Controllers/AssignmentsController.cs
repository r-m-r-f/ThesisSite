﻿using System;
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
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Assignments.Include(a => a.Group);
            return View(await applicationDbContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "ID", "ID");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,DueTo,ID,CreatedTimestamp,DeletedTimestamp,IsDeleted")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "ID", "ID", assignment.GroupId);
            return View(assignment);
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "ID", "ID", assignment.GroupId);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,DueTo,ID,CreatedTimestamp,DeletedTimestamp,IsDeleted")] Assignment assignment)
        {
            if (id != assignment.ID)
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
                    if (!AssignmentExists(assignment.ID))
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
                .FirstOrDefaultAsync(m => m.ID == id);
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
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.ID == id);
        }
    }
}
