using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LearningPlatform.Models;
using LearningPlatform.Data;
namespace LearningPlatform.Controllers;

public class UserCourseController : Controller
{
    private readonly LearningPlatformDbContext _context;
    
    public UserCourseController(LearningPlatformDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var UserCourses = _context.UserCourses.ToList();
        return View(UserCourses);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(UserCourse UserCourse)
    {
        if (ModelState.IsValid)
        {
            _context.UserCourses.Add(UserCourse);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }else 
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return View(UserCourse);
    }


}