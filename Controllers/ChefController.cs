using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // needed for .Include()
using ChefsnDishes.Models;
namespace ChefsnDishes.Controllers;

public class ChefController : Controller
{
    // the following context things are needed to inject the context service into the controller
    private MyContext _context;

    public ChefController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public IActionResult ChefMain()
    {
        List<Chef> AllChefs = _context.Chefs.Include(chef => chef.CreatedDishes).ToList();

        return View("ChefMain", AllChefs);
    }

    [HttpGet("new")]
    public IActionResult DisplayChefForm()
    {
        return View("ChefForm");
    }

    [HttpPost("createchef")]
    public IActionResult CreateChef(Chef newChef)
    {
        if(ModelState.IsValid == false)
        {
            return DisplayChefForm();
        }
        _context.Add(newChef);
        _context.SaveChanges();
        return RedirectToAction("ChefMain");
    }
}