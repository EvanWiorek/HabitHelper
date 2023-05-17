

#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HabitHelper.Models;

public class User
{
  [Key]
  public int UserId { get; set; }
  [Required(ErrorMessage = "First name is required.")]
  [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
  public string FirstName { get; set; }
  [Required(ErrorMessage = "Last name is required.")]
  [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
  public string LastName { get; set; }
  [Required(ErrorMessage = "Email is required.")]
  [EmailAddress]
  [UniqueEmail]
  public string Email { get; set; }
  [Required(ErrorMessage = "Password is required.")]
  [DataType(DataType.Password)]
  // [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
  [SpecialPassword]
  public string Password { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;

  [NotMapped]
  [Required(ErrorMessage = "Confirm Password is required.")]
  [Compare("Password")]
  [DataType(DataType.Password)]
  public string PasswordConfirm { get; set; }
  public List<Habit> MyHabits { get;set; } = new List<Habit>();
  public List<Cheer> HabitsCheeredFor { get;set; } = new List<Cheer>();
}