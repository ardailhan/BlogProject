using BlogProject.Models.Data;
using BlogProject.Models.Entity;
using BlogProject.ViewModels.Auth.Login;
using BlogProject.ViewModels.Auth.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login(string yonlen)
        {
            ViewBag.yonlen = yonlen;
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model, string yonlen) 
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password));

                if (user is not null)
                {
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("username", user.Username);
                    if (string.IsNullOrEmpty(yonlen)) return RedirectToAction("Index", "Home");
                    else return Redirect(yonlen);
                }
                else ModelState.AddModelError("", "User can not be found!");
            }
            else ModelState.AddModelError("", "Please fill all the fields!");
            return View();
        }
        public IActionResult LogOut() 
        { 
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid) 
            {
                if (!_context.Users.Any(x => x.Username.ToLower().Equals(user.Username.ToLower())))
                {
                    User newUser = new User(user.Username, user.Password);
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    TempData["message"] = "Registered Successfully";
                    return RedirectToAction("Login");
                }
                else ModelState.AddModelError("", "This username already exists");
            }
            return View();
        }
    }
}
