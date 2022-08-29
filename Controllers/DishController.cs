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
}