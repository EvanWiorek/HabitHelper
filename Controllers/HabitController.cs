using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HabitHelper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
namespace HabitHelper.Controllers;

[SessionCheck]
public class HabitController : Controller
{
  private readonly ILogger<HabitController> _logger;
  private MyContext _context;
  public HabitController(ILogger<HabitController> logger, MyContext context)
  {
    _logger = logger;
    _context = context;
  }

  [HttpGet("dashboard")]
  public IActionResult Dashboard()
  {
    int? loggedInUserId = HttpContext.Session.GetInt32("UserId");

    ViewBag.MyHabits = _context.Habits.Include(c => c.HabitCheeredBy).ThenInclude(c => c.User).Where(u => u.UserId == loggedInUserId).ToList();
    ViewBag.OtherHabits = _context.Habits.Include(c => c.Creator).Include(c => c.HabitCheeredBy).ThenInclude(c => c.User).Where(u => u.UserId != loggedInUserId).ToList();
    return View();
  }

  [HttpGet("users/{userId}")]
  public IActionResult ShowUser(int userID)
  {
    User? OneUser = _context.Users.Include(h => h.MyHabits).ThenInclude(h => h.HabitCheeredBy).ThenInclude(c => c.User).FirstOrDefault(u => u.UserId == userID);
    return View(OneUser);
  }

  [HttpGet("habits/new")]
  public IActionResult HabitForm()
  {
    return View();
  }

  [HttpPost("habits/create")]
  public IActionResult CreateHabit(Habit newHabit)
  {
    if (ModelState.IsValid)
    {
      _context.Add(newHabit);
      _context.SaveChanges();
      return RedirectToAction("Dashboard");
    }
    else
    {
      return View("HabitForm");
    }
  }

  [HttpGet("habits/{habitId}/edit")]
  public IActionResult EditHabitForm(int habitId)
  {
    Habit? oldHabit = _context.Habits.FirstOrDefault(h => h.HabitId == habitId);
    return View(oldHabit);
  }

  [HttpPost("habits/{habitId}/update")]
  public IActionResult UpdateHabit(Habit newHabit, int habitId)
  {
    Habit? oldHabit = _context.Habits.FirstOrDefault(h => h.HabitId == habitId);
    if (ModelState.IsValid)
    {
      oldHabit.Title = newHabit.Title;
      oldHabit.Description = newHabit.Description;
      oldHabit.Category = newHabit.Category;
      oldHabit.UpdatedAt = DateTime.Now;
      _context.SaveChanges();
      return RedirectToAction("Dashboard");
    }
    return View("EditHabitForm", oldHabit);
  }

    [HttpPost("habits/{habitId}/destroy")]
    public IActionResult DestroyHabit(int habitId)
    {
        Habit? habitToDelete = _context.Habits.SingleOrDefault(h => h.HabitId == habitId);
        _context.Habits.Remove(habitToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

  //Cheers route
  [HttpPost("cheers/{habitId}/create")]
  public IActionResult CheerOn(int habitId)
  {
    Cheer newCheer = new Cheer()
    {
      HabitId = habitId,
      UserId = (int)HttpContext.Session.GetInt32("UserId")
    };
    _context.Add(newCheer);
    _context.SaveChanges();
    return RedirectToAction("Dashboard");
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}