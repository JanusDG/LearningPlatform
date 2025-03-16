using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;
using LearningPlatform.Data.Service;
namespace LearningPlatform.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAll();
        return View(users);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            await _userService.Add(user);
            return RedirectToAction("Index");
        }else 
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return View(user);
    }
    public async Task<IActionResult> Remove(int id)
    {
        var user = await _userService.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
    [HttpPost]
    public async Task<IActionResult> ConfirmRemove(int id)
    {
        var user = await _userService.Find(id);
        if (user != null)
        {
            await _userService.Remove(user);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Modify(int id)
    {
        var user = await _userService.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Modify(User user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userService.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.Value = user.Value;

                await _userService.Update(existingUser);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }
        }

        return View(user);
    }

    public async Task<IActionResult> AssignSelectedCourses(int userId)
    {
        var courses = await _userService.GetAllCourses();
        
        if (courses == null || !courses.Any())
        {
            return Content("No courses found in the database.");
        }

        ViewBag.UserId = userId;
        return View(courses);
        }

    [HttpPost]
    public async Task<IActionResult> AssignToCourse(int userId, List<int> selectedCourses)
    {
        foreach (var courseId in selectedCourses)
        {
            var alreadyAssignedCourses = await _userService.GetAllUserCourses(userId);
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
                await _userService.AssignUserCourse(userId, courseId);
            }
        }
        return RedirectToAction("Index", "User");
    }

    public async Task<IActionResult> CancelSelectedCourses(int userId)
    {
        var userCourses = await _userService.GetAllUserCourses(userId);
        
        if (userCourses == null || !userCourses.Any())
        {
            return Content("No userCourses found in the database.");
        }

        ViewBag.UserId = userId;
        return View(userCourses);
        }

    [HttpPost]
    public async Task<IActionResult> RemoveUserFromCourse(int userId, List<int> selectedCourses)
    {
        foreach (var courseId in selectedCourses)
        {
            var alreadyAssignedCourses = await _userService.GetAllUserCourses(userId);
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
                await _userService.RemoveUserCourse(userId, courseId);
            }
        }
        return RedirectToAction("Index", "User");
    }



}