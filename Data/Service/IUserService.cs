using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;


namespace LearningPlatform.Data.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntityModel>> GetAll();
        Task Add(UserEntityModel user);
        Task Remove(UserEntityModel user);
        Task Update(UserEntityModel user);
        Task<UserEntityModel> Find(int id); 
        Task<IEnumerable<CourseEntityModel>> GetAllCourses();
        Task<IEnumerable<CourseEntityModel>> GetAllUserCourses(int userId);
        Task AssignUserCourse(int userId, int courseId); 
        Task RemoveUserCourse(int userId, int courseId);



    }
}