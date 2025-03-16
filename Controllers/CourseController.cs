using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;
using LearningPlatform.Data.Service;
namespace LearningPlatform.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetAll();
        return View(courses);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        if (ModelState.IsValid)
        {
            await _courseService.Add(course);
            return RedirectToAction("Index");
        }else 
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return View(course);
    }
    public async Task<IActionResult> Remove(int id)
    {
        var course = await _courseService.Find(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmRemove(int id)
    {
        var course = await _courseService.Find(id);
        if (course != null)
        {
            await _courseService.Remove(course);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Modify(int id)
    {
        var course = await _courseService.Find(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Modify(Course course)
    {
        if (ModelState.IsValid)
        {
            var existingCourse = await _courseService.Find(course.Id);
            if (existingCourse != null)
            {
                existingCourse.Value = course.Value;

                await _courseService.Update(existingCourse);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Course not found.");
            }
        }

        return View(course);
    }

        public async Task<IActionResult> AssignSelectedUsers(int courseId)
    {
        var users = await _courseService.GetAllUsers();
        
        if (users == null || !users.Any())
        {
            return Content("No users found in the database.");
        }

        ViewBag.CourseId = courseId;
        return View(users);
        }

    [HttpPost]
    public async Task<IActionResult> AssignToUser(int courseId, List<int> selectedUsers)
    {
        foreach (var userId in selectedUsers)
        {
            var alreadyAssignedUsers = await _courseService.GetAllCourseUsers(courseId);
            var ifAdd = true;
            foreach (var existingUser in alreadyAssignedUsers)
            {
                if (existingUser.Id == userId)
                {
                    ifAdd = false;
                    break;
                }  
            }
            if (ifAdd)
            {
                await _courseService.AssignCourseUser(courseId, userId);
            }
        }
        return RedirectToAction("Index", "Course");
    }

    public async Task<IActionResult> CancelSelectedUsers(int courseId)
    {
        var courseUsers = await _courseService.GetAllCourseUsers(courseId);
        
        if (courseUsers == null || !courseUsers.Any())
        {
            return Content("No courseUsers found in the database.");
        }

        ViewBag.CourseId = courseId;
        return View(courseUsers);
        }

    [HttpPost]
    public async Task<IActionResult> RemoveCourseFromUser(int courseId, List<int> selectedUsers)
    {
        foreach (var userId in selectedUsers)
        {
            var alreadyAssignedUsers = await _courseService.GetAllCourseUsers(courseId);
            var ifRemove = false;
            foreach (var existingUser in alreadyAssignedUsers)
            {
                if (existingUser.Id == userId)
                {
                    ifRemove = true;
                    break;
                }  
            }
            if (ifRemove)
            {
                await _courseService.RemoveCourseUser(courseId, userId);
            }
        }
        return RedirectToAction("Index", "Course");
    }


}