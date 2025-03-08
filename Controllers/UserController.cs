using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LearningPlatform.Models;
using LearningPlatform.Data;
namespace LearningPlatform.Controllers;

public class UserController : Controller
{
    private readonly LearningPlatformDbContext _context;
    
    public UserController(LearningPlatformDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
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
        return View(user);
    }


}