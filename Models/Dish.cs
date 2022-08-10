#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsnDishes.Models;

public class Dish
{
    [Key]
    public int DishId {get; set;}
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name of Dish:")]
    public string Name {get; set;}

    // Have to add question marks by the int types because it is not getting past the first round of validations
    [Required(ErrorMessage = "Tastiness is required")]
    [Range(1, 5)]
    [Display(Name = "Tastiness:")]
    public int? Tastiness {get; set;}

    [Required(ErrorMessage = "Calories are required")]
    [Display(Name = "# of Calories:")]
    public int? Calories {get; set;}

    [Required(ErrorMessage = "Description is required")]
    [Display(Name = "Description:")]
    public string Description {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    [Required(ErrorMessage = "You must select a chef")]
    [Display(Name = "Chef")]
    public int ChefId {get;set;}

    // like the reverse of the list in chef class. This will have the creator. This is pointing to a whole chef instance.
    public Chef? Creator {get;set;}
}