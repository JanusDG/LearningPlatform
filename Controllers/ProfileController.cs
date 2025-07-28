using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearningPlatform.Data.Service;
using LearningPlatform.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Helpers;
using System.Security.Claims;




namespace LearningPlatform.Controllers
{

    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userAssignedCourses = await _userService.GetAllUserCoursesAsync(userId);
            var courseVMs = userAssignedCourses.Select(course => ViewEntityMapper.GetCourseViewIdModel(course)).ToList();
            return View(courseVMs);
        }
        
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CoursePage(int courseId)
        {
            // is this okay, or should I use a user service?
            var courses = await _userService.GetAllCoursesAsync();
            var course = courses
                .FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                return NotFound();
            }

            var courseVM = ViewEntityMapper.GetCourseViewIdModel(course);
            ViewBag.CourseId = courseId;
            return View(courseVM);
        }
        

    }
}