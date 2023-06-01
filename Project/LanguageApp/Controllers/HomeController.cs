using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanguageApp.Models;

namespace LanguageApp.Controllers;

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

    public IActionResult Login(LoginVM model)
    {
        string Username = model.Username;
        string Password = model.Password;
        return View("Login", model);
    }
}
