using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LearningPlatform.Models;
using LearningPlatform.Data;
namespace LearningPlatform.Controllers;

public class CourseController : Controller
{
    private readonly LearningPlatformDbContext _context;
    
    public CourseController(LearningPlatformDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var courses = _context.Courses.ToList();
        return View(courses);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Add(course);
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
        return View(course);
    }


}