using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;


namespace LearningPlatform.Data.Service
{
    public class CourseService: ICourseService
    {
        private readonly LearningPlatformDbContext _context;
        public CourseService(LearningPlatformDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> GetAll()
        {
            var courses = await _context.Courses.ToListAsync();
            return courses; 
        }
        public async Task Add(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Course> Find(int id) 
        {
            var CourseById = await _context.Courses.FindAsync(id);
            return CourseById;
        }
        public async Task Update(Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.Id);
            if (existingCourse != null)
            {
                _context.Entry(existingCourse).CurrentValues.SetValues(course);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users; 
        }
        public async Task<IEnumerable<User>> GetAllCourseUsers(int courseId)
        {
            var courseUsers = await _context.UserCourses
                .Where(uc => uc.CourseId == courseId)
                .Join(_context.Users,
                    uc => uc.UserId,
                    c => c.Id,
                    (uc, c) => c )
                .ToListAsync();
            return courseUsers; 
        }
        public async Task AssignCourseUser(int courseId, int userId)
        {
            _context.UserCourses.Add(new UserCourse { CourseId = courseId, UserId = userId });
            await _context.SaveChangesAsync();

        }

        public async Task RemoveCourseUser(int courseId, int userId)
        {
            var courseUser = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.CourseId == courseId && uc.UserId == userId);

            if (courseUser != null)
            {
                _context.UserCourses.Remove(courseUser);
                await _context.SaveChangesAsync();
            }

        }



    }
}