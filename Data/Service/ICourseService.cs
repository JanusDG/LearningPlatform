using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;


namespace LearningPlatform.Data.Service
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseEntityModel>> GetAllCoursesAsync();
        Task AddCourseAsync(CourseEntityModel course);
        Task RemoveCourseAsync(CourseEntityModel course);
        Task RemoveCourseByIDAsync(int id);
        Task UpdateCourseAsync(CourseEntityModel course);
        Task UpdateCourseByIDAsync(int id, CourseViewModel VM);
        Task<CourseEntityModel> FindCourseByIdAsync(int Id); 
        Task<IEnumerable<UserEntityModel>> GetAllUsersAsync();
        Task<IEnumerable<UserEntityModel>> GetAllCourseUsersAsync(int courseId);
        Task AssignCourseUserAsync(int courseId, int userId);
        Task RemoveCourseUserAsync(int courseId, int userId);


    }
}