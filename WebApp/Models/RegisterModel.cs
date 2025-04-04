using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class RegisterModel
{
    [Display(Name = "First Name", Prompt = "Your First Name")]
    [Required(ErrorMessage = "First Name is required")]
    [DataType(DataType.Text)]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed in the First Name.")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Your Last Name")]
    [Required(ErrorMessage = "Last Name is required")]
    [DataType(DataType.Text)]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed in the Last Name.")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Your Email")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Your Password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Min 8 chars with upper, lower, number & symbol.")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm Password", Prompt = "Confirm Your Password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "You need to accept this")]
    public bool TermsAndConditions { get; set; }
}
