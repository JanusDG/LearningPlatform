using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;


namespace LearningPlatform.Data.Service
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseEntityModel>> GetAll();
        Task Add(CourseEntityModel course);
        Task Remove(CourseEntityModel course);
        Task Update(CourseEntityModel course);
        Task<CourseEntityModel> Find(int Id); 
        Task<IEnumerable<UserEntityModel>> GetAllUsers();
        Task<IEnumerable<UserEntityModel>> GetAllCourseUsers(int courseId);
        Task AssignCourseUser(int courseId, int userId);
        Task RemoveCourseUser(int courseId, int userId);


    }
}