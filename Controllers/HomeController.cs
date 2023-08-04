using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _10_Session_Workshop.Models;

namespace _10_Session_Workshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("Login")]
    public IActionResult Login(string Name)
    {
        if(Name == null)
        {
            return RedirectToAction("Index");
        }
        HttpContext.Session.SetString("Name", Name);
        HttpContext.Session.SetInt32("Number", 0);
        return RedirectToAction("Dashboard");
    }

    [HttpGet("Logout")]
    public IActionResult LogOut()
    {
        HttpContext.Session.Remove("Name");
        HttpContext.Session.SetInt32("Number", 0);
        return RedirectToAction("Index");
    }

    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("Name") == null)
        {
            return RedirectToAction("Index");
        }
        // int? IntVariable = HttpContext.Session.GetInt32("Number");
        return View();
    }

    [HttpGet("modifyNumber/{modifier}")]
    public IActionResult ModifyNumber(string modifier)
    {
        Console.WriteLine("Modify Number");
        Console.WriteLine(modifier);
        if (HttpContext.Session.GetString("Name") == null)
        {
            return RedirectToAction("Index");
        }

        Random random = new Random();
        int number = Convert.ToInt32(HttpContext.Session.GetInt32("Number"));

        if (modifier == "+1")
        {
            number+=1;
            HttpContext.Session.SetInt32("Number", number);
        }
        else if (modifier == "-1")
        {
            number-=1;
            HttpContext.Session.SetInt32("Number", number);
        }
        else if (modifier == "x2")
        {
            number *= 2;
            HttpContext.Session.SetInt32("Number", number);
        }
        else if (modifier == "rand")
        {
            int randInt = random.Next(1,11);
            number += randInt;
            HttpContext.Session.SetInt32("Number", number);
        }

        return RedirectToAction("Dashboard");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
