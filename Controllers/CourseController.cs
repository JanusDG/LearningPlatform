using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;
using LearningPlatform.Data.Service;
namespace LearningPlatform.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    
    private static CourseViewModel GetCourseViewModel(CourseEntityModel course)
    {
        return new CourseViewModel
        {
            UserCourses = course.UserCourses,
            Name = course.Name,
            Description = course.Description,
            Lessons = course.Lessons,
        };
    }

    private static List<CourseViewModel> GetCourseViewModels(List<CourseEntityModel> courses)
    {
        return courses.Select(courseEM => 
            new CourseViewModel
                {
                    UserCourses = courseEM.UserCourses,
                    Name = courseEM.Name,
                    Description = courseEM.Description,
                    Lessons = courseEM.Lessons,
                }
            ).ToList();
    }

    private static UserViewModel GetUserViewModel(UserEntityModel user)
    {
        return new UserViewModel
        {
            UserCourses = user.UserCourses,
            Username = user.Username,
            Firstname = user.Firstname,
            Surname = user.Surname,
            Email = user.Email,
            Password = user.Password,
        };
    }

    private static List<UserViewModel> GetUserViewModels(List<UserEntityModel> users)
    {
        return users.Select(userEM => 
            new UserViewModel
                {
                    UserCourses = userEM.UserCourses,
                    Username = userEM.Username,
                    Firstname = userEM.Firstname,
                    Surname = userEM.Surname,
                    Email = userEM.Email,
                    Password = userEM.Password,
                }
            ).ToList();
    }

    private static CourseEntityModel GetCourseEntityModel(CourseViewModel course)
    {
        return new CourseEntityModel
        {
            UserCourses = course.UserCourses,
            Name = course.Name,
            Description = course.Description,
            Lessons = course.Lessons,
        };
    }

    private static List<CourseEntityModel> GetCourseEntityModels(List<CourseViewModel> courses)
    {
        return courses.Select(courseEM => 
            new CourseEntityModel
                {
                    UserCourses = courseEM.UserCourses,
                    Name = courseEM.Name,
                    Description = courseEM.Description,
                    Lessons = courseEM.Lessons,
                }
            ).ToList();
    }

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
    public async Task<IActionResult> Create(CourseViewModel courseVM)
    {
        if (ModelState.IsValid)
        {
            await _courseService.Add(GetCourseEntityModel(courseVM));
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
        var courseEM = await _courseService.Find(id);
        if (courseEM == null)
        {
            return NotFound();
        }
        var courseVM = GetCourseViewModel(courseEM);
        return View(courseVM);
    }

    [HttpPost("ConfirmRemove/{id}")]
    public async Task<IActionResult> ConfirmRemove(int id)
    {
        var courseEM = await _courseService.Find(id);
        if (courseEM != null)
        {
            await _courseService.Remove(courseEM);
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
        var courseVM = GetCourseViewModel(course);
        return View(courseVM);
    }

    [HttpPost("SubmitModify/{id}")]
    public async Task<IActionResult> SubmitModify(int id, CourseViewModel updatedCourse)
    {
        if (ModelState.IsValid)
        {
            var existingCourse = await _courseService.Find(id);
            if (existingCourse != null)
            {
                existingCourse.Name = updatedCourse.Name;

                await _courseService.Update(existingCourse);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Course not found.");
            }
        }

        return View(updatedCourse);
    }

    public async Task<IActionResult> AssignSelectedUsers(int courseId)
    {
        var userEMs = await _courseService.GetAllUsers();
        // var userVMs = GetUserViewModels(userEMs);
        var userVMs = userEMs; // TODO REMOVE THIS LATER
        
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