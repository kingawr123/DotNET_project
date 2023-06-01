using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanguageApp.Models;

namespace LanguageApp.Controllers;

public class HomeController : Controller
{
    private bool _loggedin = true;

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
        if (HttpContext.Session.GetString("UserId") == HttpContext.Session.GetString("Admin")){
            TempData["Message"] ="witaj adminie!";
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
        /*string Username = model.Username;
        string Password = model.Password;
        bool authenticationSuccessful = true;
        if (authenticationSuccessful)
        {
            HttpContext.Session.SetString("IsLoggedIn", "true");
            return RedirectToAction("LoggedIn");
        }
        return View("Login", model);*/
        return RedirectToAction("Create", "Login");
    }

    public IActionResult LogOut(){
        HttpContext.Session.SetString("IsLoggedIn", "false");
        HttpContext.Session.SetString("UserId", "");
        return RedirectToAction("Index");
    }
}
