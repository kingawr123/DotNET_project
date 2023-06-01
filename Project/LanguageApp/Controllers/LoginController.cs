using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageApp.Data;
using LanguageApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace LanguageApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly LanguageAppContext _context;

        private string HashString(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Compute the hash of the input bytes
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert the hashed bytes to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        private bool VerifyHash(string input, string hashedInput)
        {
            string hashedInputToVerify = HashString(input);
            return StringComparer.OrdinalIgnoreCase.Compare(hashedInputToVerify, hashedInput) == 0;
        }

        public LoginController(LanguageAppContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'LanguageAppContext.User'  is null.");
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var User = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Password")] User User)
        {
            
            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            // Console.WriteLine(errors.ToList()[0].ErrorMessage);
            
            if (ModelState.IsValid && !HttpContext.Session.Keys.Contains("pierwszy_request"))
            {
                User.Password = HashString(User.Password);
                _context.Add(User);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("UserId", User.UserId.ToString());
                HttpContext.Session.SetString("Admin", User.UserId.ToString());
                HttpContext.Session.SetString("pierwszy_request", "false");
                return RedirectToAction("Index", "Home");
            } else if( ModelState.IsValid && (HttpContext.Session.GetString("UserId") == HttpContext.Session.GetString("Admin"))){
                _context.Add(User);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            } else if(ModelState.IsValid){
                if ( _context.User == null)
                {       
                    return NotFound();
                }
                User user = _context.User.FirstOrDefault(u => u.Username == User.Username);
                if (user == null)
                {
                    TempData["Message"] ="Nie znaleziono użytkownika!";
                    return View(User);
                }
                if (VerifyHash(user.Password, User.Password )){
                    TempData["Message"] ="Niepoprawne hasło!";
                    return View(User);
                }
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("UserId", User.UserId.ToString());
                return RedirectToAction("Index", "Home");

            }
            return View(User);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Password")] User User)
        {
            if (id != User.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(User);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(User.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(User);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var User = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'LanguageAppContext.User'  is null.");
            }
            var User = await _context.User.FindAsync(id);
            if (User != null)
            {
                _context.User.Remove(User);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
