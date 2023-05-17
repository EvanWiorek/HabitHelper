#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace HabitHelper.Models;

public class LoginUser
{
  [Required(ErrorMessage = "An email address is required.")]
  [EmailAddress]
  public string LogEmail { get; set; }
  [Required(ErrorMessage = "A password is required.")]
  [DataType(DataType.Password)]
  public string LogPassword { get; set; }
}