
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class SpecialPasswordAttribute : ValidationAttribute
{
  protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
  {
    bool passwordIsValid = true;
    string reason = String.Empty;
    string Password = value == null ? String.Empty : value.ToString();
    Regex reSymbol = new Regex("[^a-zA-Z0-9]");
    Regex reNumber = new Regex("[^0-9]");

    if (String.IsNullOrEmpty(Password) || Password.Length < 8)
    {
      passwordIsValid = false;
      reason = "Your password must be at least 8 characters long. ";
    }
    if (!reSymbol.IsMatch(Password))
    {
      passwordIsValid = false;
      reason += "Your password must contain at least 1 symbol character. ";
    }
    if (!reNumber.IsMatch(Password))
    {
      passwordIsValid = false;
      reason += "Your password must contain at least 1 number. ";
    }
    if (passwordIsValid)
    {
      return ValidationResult.Success;
    }
    else
    {
      return new ValidationResult(reason);
    }
  }
}