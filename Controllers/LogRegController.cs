using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HabitHelper.Models;
using Microsoft.AspNetCore.Identity;
namespace HabitHelper.Controllers;

public class LogRegController : Controller
{
  private readonly ILogger<LogRegController> _logger;
  private MyContext _context;
  public LogRegController(ILogger<LogRegController> logger, MyContext context)
  {
    _logger = logger;
    _context = context;
  }
  [HttpGet("")]
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost("users/create")]
  public IActionResult CreateUser(User newUser)
  {
    if (ModelState.IsValid)
    {
      PasswordHasher<User> Hasher = new PasswordHasher<User>();
      newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
      _context.Add(newUser);
      _context.SaveChanges();
      HttpContext.Session.SetInt32("UserId", newUser.UserId);
      HttpContext.Session.SetString("FirstName", newUser.FirstName);
      return RedirectToAction("Dashboard", "Habit");
    }
    else
    {
      return View("Index");
    }
  }

  [HttpGet("users/login")]
  public IActionResult Login(LoginUser userSubmission)
  {
    if (ModelState.IsValid)
    {
      User? userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.LogEmail);
      if (userInDb == null)
      {
        ModelState.AddModelError("LogEmail", "Invalid Email/Password");
        return View("Index");
      }
      PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
      var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LogPassword);
      if (result == 0)
      {
        ModelState.AddModelError("LogEmail", "Invalid Email/Password");
        return View("Index");
      }
      HttpContext.Session.SetInt32("UserId", userInDb.UserId);
      HttpContext.Session.SetString("FirstName", userInDb.FirstName);
      return RedirectToAction("Dashboard", "Habit");
    }
    else
    {
      return View("Index");
    }
  }

  [HttpPost("logout")]
  public IActionResult Logout()
  {
    HttpContext.Session.Clear();
    return RedirectToAction("Index");
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }  
}
