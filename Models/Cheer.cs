#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace HabitHelper.Models;

public class Cheer
{
  [Key]
  public int CheerId { get;set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public int UserId { get;set; }
  public int HabitId { get;set; }
  public User? User { get;set; }
  public Habit? Habit { get;set; }
}