#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace HabitHelper.Models;

public class Habit
{
  [Key]
  public int HabitId { get;set; }
  [Required(ErrorMessage = "Title is required.")]
  public string Title { get;set; }
  [Required(ErrorMessage = "Description is required.")]
  public string Description { get;set; }
  [Required(ErrorMessage = "Category is required.")]
  public string Category { get;set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
  public int UserId { get;set; }
  public User? Creator { get;set; }
  public List<Cheer> HabitCheeredBy { get;set; } = new List<Cheer>();
}