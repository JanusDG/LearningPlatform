using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;


namespace LearningPlatform.Data.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntityModel>> GetAllAsync();
        Task AddAsync(UserEntityModel user);
        Task RemoveAsync(UserEntityModel user);
        Task UpdateAsync(UserEntityModel user);
        Task UpdateUserByIDAsync(int id, UserViewModel user);
        Task<UserEntityModel> FindUserByIdAsync(int id); 
        Task<IEnumerable<CourseEntityModel>> GetAllCoursesAsync();
        Task<IEnumerable<CourseEntityModel>> GetAllUserCoursesAsync(int userId);
        Task AssignUserCourseAsync(int userId, int courseId); 
        Task RemoveUserCourseAsync(int userId, int courseId);



    }
}