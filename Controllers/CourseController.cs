using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;
using LearningPlatform.Data.Service;
using LearningPlatform.Helpers;
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
        var courses = await _courseService.GetAllCoursesAsync();
        var courseVMs = courses.Select(course => ViewEntityMapper.GetCourseViewIdModel(course)).ToList();
        return View(courseVMs);
    }

    public IActionResult Create()
    {
        var model = new CourseViewModel
        {
            //todo add some string hash for the default value for Name
            Name = "My Course",
            Description = "A really nice course"
        };

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CourseViewModel courseVM)
    {
        if (ModelState.IsValid)
        {
            var courseEM = ViewEntityMapper.GetCourseEntityModel(courseVM);
            await _courseService.AddCourseAsync(courseEM);
            return RedirectToAction("Index");
        }else 
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return View(courseVM);
    }
    public async Task<IActionResult> Remove(int id)
    {
        var courseEM = await _courseService.FindCourseByIdAsync(id);
        if (courseEM == null)
        {
            return NotFound();
        }
        var courseVM = ViewEntityMapper.GetCourseViewModel(courseEM);
        return View(courseVM);
    }

    [HttpPost("ConfirmRemove/{id}")]
    public async Task<IActionResult> ConfirmRemove(int id)
    {
        await _courseService.RemoveCourseByIDAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Modify(int id)
    {
        var course = await _courseService.FindCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        var courseVM = ViewEntityMapper.GetCourseViewModel(course);
        return View(courseVM);
    }

    [HttpPost("SubmitModify/{id}")]
    public async Task<IActionResult> SubmitModify(int id, CourseViewModel updatedCourse)
    {
        
        if (ModelState.IsValid)
        {
            await _courseService.UpdateCourseByIDAsync(id, updatedCourse);
            return RedirectToAction("Index");   
        }else 
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        
        return View(updatedCourse);
    }

    public async Task<IActionResult> AssignSelectedUsers(int courseId)
    {
        var userEMs = await _courseService.GetAllUsersAsync();
        var userVMs = userEMs.Select(user => ViewEntityMapper.GetUserViewIdModel(user)).ToList();
        if (userVMs == null || !userVMs.Any())
        {
            return Content("No users found in the database.");
        }

        ViewBag.CourseId = courseId;
        return View(userVMs);
        }

    [HttpPost]
    public async Task<IActionResult> AssignToUser(int courseId, List<int> selectedUsersIds)
    {
        foreach (var userId in selectedUsersIds)
        {
            var alreadyAssignedUsers = await _courseService.GetAllCourseUsersAsync(courseId);
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
                await _courseService.AssignCourseUserAsync(courseId, userId);
            }
        }
        return RedirectToAction("Index", "Course");
    }

    public async Task<IActionResult> CancelSelectedUsers(int courseId)
    {
        var courseUsersEMs = await _courseService.GetAllCourseUsersAsync(courseId);
        var courseUsers = courseUsersEMs.Select(user => ViewEntityMapper.GetUserViewIdModel(user)).ToList();
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
            var alreadyAssignedUsers = await _courseService.GetAllCourseUsersAsync(courseId);
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
                await _courseService.RemoveCourseUserAsync(courseId, userId);
            }
        }
        return RedirectToAction("Index", "Course");
    }


}