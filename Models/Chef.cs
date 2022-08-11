#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsnDishes.Models;

public class Chef
{
    [Key]
    public int ChefId {get;set;}
    
    [Required(ErrorMessage = "First name is required")]
    [Display(Name = "First Name")]
    public string FirstName {get;set;}

    [Required(ErrorMessage = "Last name is required")]
    [Display(Name = "Last Name")]
    public string LastName {get;set;}

    [Required(ErrorMessage = "Birth date is required")]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    [PastDates]
    public DateTime? DoB {get;set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public List<Dish> CreatedDishes {get; set;} = new List<Dish>();
}


// custom validation to check if the DOB of the chefs is a past date.
public class PastDatesAttribute : ValidationAttribute
{
    // Need the question marks to account for any possible null values
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        DateTime DoB = (DateTime)value;
        if(DateTime.Now <= DoB)
        {
            return new ValidationResult("You must provide a past date.");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}