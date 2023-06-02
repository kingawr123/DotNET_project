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
        if (HttpContext.Session.GetString("UserId") == HttpContext.Session.GetString("Admin")){
            TempData["Message"] ="Witaj" + HttpContext.Session.GetString("UserId");
            return RedirectToAction("AdminLoggedIn");
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

    public IActionResult AdminLoggedIn(){
        return View();
    }

    public IActionResult Login(User model)
    {
        return RedirectToAction("Create", "Login");
    }

    public IActionResult LogOut(){
        HttpContext.Session.SetString("IsLoggedIn", "false");
        HttpContext.Session.SetString("UserId", "");
        return RedirectToAction("Index", "Home");
    }
}
