using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // needed for .Include()
using ChefsnDishes.Models;

namespace ChefsnDishes.Controllers;

public class DishController : Controller
{
    // the following context things are needed to inject the context service into the controller
    private MyContext _context;

    public DishController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("dishes")]
    public IActionResult Main()
    {
        List<Dish> AllDishes = _context.Dishes.Include(dish => dish.Creator).OrderByDescending(dish => dish.CreatedAt).ToList();
        return View("Main", AllDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult DisplayForm()
    {
        ViewBag.AllChefs = _context.Chefs.OrderByDescending(Chef => Chef.CreatedAt);
        return View("Form");
    }

    [HttpPost("create")]
    public IActionResult CreateDish(Dish newDish)
    {
        Chef? creator = _context.Chefs.FirstOrDefault(c => c.ChefId == newDish.ChefId);
        if(creator == null)
        {
            ModelState.AddModelError("ChefId", "Must select a chef");
        }
        if(ModelState.IsValid == false)
        {
            return DisplayForm();
        }
        
        _context.Add(newDish);
        _context.SaveChanges();
        return RedirectToAction("Main");
    }

    // [HttpGet("{DishId}")]
    // public IActionResult Details(int DishId)
    // {
    //     Dish? OneDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
    //     if(OneDish == null)
    //     {
    //         return RedirectToAction("Main");
    //     }
    //     return View("Details", OneDish);
    // }

    // [HttpGet("delete/{DishId}")]
    // public IActionResult DeleteDish(int DishId)
    // {
    //     // using Single rather than First because the latter can sometimes cause errors with the delete function
    //     Dish? OneDish = _context.Dishes.SingleOrDefault(dish => dish.DishId == DishId);
    //     if(OneDish == null)
    //     {
    //         return RedirectToAction("Details", DishId);
    //     }
    //     _context.Dishes.Remove(OneDish);
    //     _context.SaveChanges();
    //     return RedirectToAction("Main");
    // }

    // [HttpGet("edit/{DishId}")]
    // public IActionResult EditDish(int DishId)
    // {
    //     Dish? OneDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
    //     if(OneDish == null)
    //     {
    //         return RedirectToAction("Main");
    //     }
    //     return View("EditPage", OneDish);
    // }

    // [HttpPost("edit/{DishId}/updated")]
    // public IActionResult EditDishSubmit(int DishId, Dish editedDish)
    // {
    //     // checking form validations
    //     if(ModelState.IsValid == false)
    //     {
    //         return EditDish(DishId);
    //     }
    //     // if the form validations pass, then it will do the following to grab the selection by ID, make sure to handle it if that selection is null, but continue with getting the values to update if it is not null.
    //     Dish? OneDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
    //     if(OneDish == null)
    //     {
    //         return RedirectToAction("Main");
    //     }

    //     OneDish.Name = editedDish.Name;
    //     OneDish.Chef = editedDish.Chef;
    //     OneDish.Tastiness = editedDish.Tastiness;
    //     OneDish.Calories = editedDish.Calories;
    //     OneDish.Description = editedDish.Description;
    //     OneDish.UpdatedAt = DateTime.Now;
    //     _context.Dishes.Update(OneDish);
    //     _context.SaveChanges();
    //     // have to pass the ID in as an anonymous object when using this format.
    //     return RedirectToAction("Details", new {DishId = OneDish.DishId});
    // }
}