using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;


namespace LearningPlatform.Data.Service
{
    public class UserService: IUserService
    {
        private readonly LearningPlatformDbContext _context;
        public UserService(LearningPlatformDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserEntityModel>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users; 
        }
        public async Task Add(UserEntityModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(UserEntityModel user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserEntityModel> Find(int id) 
        {
            var userById = await _context.Users.FindAsync(id);
            return userById;
        }
        public async Task Update(UserEntityModel user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<CourseEntityModel>> GetAllCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            return courses; 
        }
        public async Task<IEnumerable<CourseEntityModel>> GetAllUserCourses(int userId)
        {
            var userCourses = await _context.UserCourses
                .Where(uc => uc.UserId == userId)
                .Join(_context.Courses,
                    uc => uc.CourseId,
                    c => c.Id,
                    (uc, c) => c )
                .ToListAsync();
            return userCourses; 
        }
        public async Task AssignUserCourse(int userId, int courseId)
        {
            _context.UserCourses.Add(new UserCourse { UserId = userId, CourseId = courseId });
            await _context.SaveChangesAsync();

        }

        public async Task RemoveUserCourse(int userId, int courseId)
        {
            var userCourse = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (userCourse != null)
            {
                _context.UserCourses.Remove(userCourse);
                await _context.SaveChangesAsync();
            }

        }

        

    }
}