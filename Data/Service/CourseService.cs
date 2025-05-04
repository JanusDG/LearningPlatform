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


        public async Task<IEnumerable<CourseEntityModel>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return courses; 
        }
        public async Task AddCourseAsync(CourseEntityModel course)
        // TODO
        // ensure  unique naming of courses
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCourseAsync(CourseEntityModel course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveCourseByIDAsync(int id){
            var course = await FindCourseByIdAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CourseEntityModel> FindCourseByIdAsync(int id) 
        {
            var courseById = await _context.Courses.FindAsync(id);
            return courseById;
        }

        // TODO rename all async methods to end with async like this one
        public async Task UpdateCourseAsync(CourseEntityModel course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.Id);
            if (existingCourse != null)
            {
                _context.Entry(existingCourse).CurrentValues.SetValues(course);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateCourseByIDAsync(int id, CourseViewModel updatedCourse)
        {
            var existingCourse = await FindCourseByIdAsync(id);
            if (existingCourse != null)
            {
                _context.Entry(existingCourse).CurrentValues.SetValues(updatedCourse);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<UserEntityModel>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users; 
        }
        public async Task<IEnumerable<UserEntityModel>> GetAllCourseUsersAsync(int courseId)
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
        public async Task AssignCourseUserAsync(int courseId, int userId)
        {
            _context.UserCourses.Add(new UserCourse { CourseId = courseId, UserId = userId });
            await _context.SaveChangesAsync();

        }

        public async Task RemoveCourseUserAsync(int courseId, int userId)
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