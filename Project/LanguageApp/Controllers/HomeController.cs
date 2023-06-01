using Microsoft.AspNetCore.Mvc;
using LanguageApp.Models;
using LoginController = LanguageApp.Controllers.LoginController;


namespace LanguageApp.Controllers;

public class HomeController : Controller
{
    private bool _loggedin;

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
        if (HttpContext.Session.Keys.Contains("pierwszy_request")){
            TempData["Message"] =HttpContext.Session.GetString("UserId") == HttpContext.Session.GetString("Admin");
        }
        if (isLoggedIn == "true")
        {
           return RedirectToAction("LoggedIn");
        }
        else
        {
            return View();
        }
    }

    public IActionResult LoggedIn(){
        return View();
    }

    public IActionResult Login(User model)
    {
        string Username = model.Username;
        string Password = model.Password;
        _loggedin = true;
        return View();
    }
}
