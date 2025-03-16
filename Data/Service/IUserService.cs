using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;


namespace LearningPlatform.Data.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task Add(User user);
        Task Remove(User user);
        Task Update(User user);
        Task<User> Find(int id); 
        Task<IEnumerable<Course>> GetAllCourses();
        Task<IEnumerable<Course>> GetAllUserCourses(int userId);
        Task AssignUserCourse(int userId, int courseId); 
        Task RemoveUserCourse(int userId, int courseId);



    }
}