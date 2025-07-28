using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;
using LearningPlatform.Data.Service;
using LearningPlatform.Helpers;
using LearningPlatform.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace LearningPlatform.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync();
        var userVMs = users.Select(user => ViewEntityMapper.GetUserViewIdModel(user)).ToList();
        return View(userVMs);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        var model = new UserEntityModel
        {
            //todo add some string hash for the default value for Username
            Username = "johndoe",
            Firstname = "John",
            Role = "User",
            Surname = "Doe",
            Email = "john@example.com",
            Password = "123456"
        };

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(UserEntityModel userEM)
    {
        if (ModelState.IsValid)
        {
            userEM.Password = PasswordHashHandler.HashPassword(userEM.Password);
            await _userService.AddAsync(userEM);
            return RedirectToAction("Index");
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return View(userEM);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Remove(int id)
    {
        var userEM = await _userService.FindUserByIdAsync(id);
        if (userEM == null)
        {
            return NotFound();
        }
        var userVM = ViewEntityMapper.GetUserViewModel(userEM);
        return View(userVM);
    }

    [HttpPost("Course/ConfirmRemove/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ConfirmRemove(int id)
    {
        var userEM = await _userService.FindUserByIdAsync(id);
        if (userEM != null)
        {
            await _userService.RemoveAsync(userEM);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Modify(int id)
    {
        var user = await _userService.FindUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var userVM = ViewEntityMapper.GetUserViewModel(user);
        return View(userVM);
    }

    [HttpPost("User/SubmitModify/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SubmitModify(int id, UserViewModel updatedUser)
    {
        if (ModelState.IsValid)
        {
            await _userService.UpdateUserByIDAsync(id, updatedUser);
            return RedirectToAction("Index");
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }

        return View(updatedUser);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignSelectedCourses(int userId)
    {
        var courses = await _userService.GetAllCoursesAsync();
        var courseVMs = courses.Select(course => ViewEntityMapper.GetCourseViewIdModel(course)).ToList();

        if (courseVMs == null || !courseVMs.Any())
        {
            return Content("No courses found in the database.");
        }

        ViewBag.UserId = userId;
        return View(courseVMs);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignToCourse(int userId, List<int> selectedCourses)
    {
        foreach (var courseId in selectedCourses)
        {
            var alreadyAssignedCourses = await _userService.GetAllUserCoursesAsync(userId);
            var ifAdd = true;
            foreach (var existingCourse in alreadyAssignedCourses)
            {
                if (existingCourse.Id == courseId)
                {
                    ifAdd = false;
                    break;
                }
            }
            if (ifAdd)
            {
                await _userService.AssignUserCourseAsync(userId, courseId);
            }
        }
        return RedirectToAction("Index", "User");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CancelSelectedCourses(int userId)
    {
        var userCourseEMs = await _userService.GetAllUserCoursesAsync(userId);
        var courseVMs = userCourseEMs.Select(course => ViewEntityMapper.GetCourseViewIdModel(course)).ToList();
        if (courseVMs == null || !courseVMs.Any())
        {
            return Content("No courses found in the database for the user.");
        }

        ViewBag.UserId = userId;
        return View(courseVMs);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveUserFromCourse(int userId, List<int> selectedCourses)
    {
        foreach (var courseId in selectedCourses)
        {
            var alreadyAssignedCourses = await _userService.GetAllUserCoursesAsync(userId);
            var ifRemove = false;
            foreach (var existingCourse in alreadyAssignedCourses)
            {
                if (existingCourse.Id == courseId)
                {
                    ifRemove = true;
                    break;
                }
            }
            if (ifRemove)
            {
                await _userService.RemoveUserCourseAsync(userId, courseId);
            }
        }
        return RedirectToAction("Index", "User");
    }



}