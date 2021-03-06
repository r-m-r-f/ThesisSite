﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThesisSite.Data;
using ThesisSite.Domain;
using ThesisSite.Services.Interface;

namespace ThesisSite.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGroupsService _groupsService;

        public CourseService(ApplicationDbContext context, IGroupsService groupsService)
        {
            _context = context;
            _groupsService = groupsService;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context.Courses.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _context.Courses
                .SingleOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        }

        public bool Exists(int id)
        {
            return _context.Courses.Any(e => e.Id == id && !e.IsDeleted);
        }

        public async Task AddCourse(Course course)
        {
            course.CreatedTimestamp = DateTime.UtcNow;
            _context.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(int id)
        {
            var course = await GetCourseById(id);

            if (course == null)
            {
                return;
            }

            if (course != null)
            {
                var groups = course.Groups?.ToImmutableList();

                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        await _groupsService.RemoveGroup(group.Id);
                    }
                }

                _context.Remove(course);

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCourse(Course course)
        {
            _context.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}
