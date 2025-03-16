using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;


namespace LearningPlatform.Data.Service
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAll();
        Task Add(Course course);
        Task Remove(Course course);
        Task Update(Course course);
        Task<Course> Find(int Id); 
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<User>> GetAllCourseUsers(int courseId);
        Task AssignCourseUser(int courseId, int userId);
        Task RemoveCourseUser(int courseId, int userId);


    }
}