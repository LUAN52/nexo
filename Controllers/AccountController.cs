using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using nexo.Models;
using Nexo.data;

namespace Nexo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Client> _user;
        private readonly SignInManager<Client> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<Client> user, SignInManager<Client> singManager, AppDbContext context)
        {
            _user = user;
            _signInManager = singManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {  
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            if ((string.IsNullOrWhiteSpace(email)) ||(string.IsNullOrWhiteSpace(name)))
            {   
                System.Console.WriteLine("vazio");
                return RedirectToAction("Register", "Account");
            }
           

            var client = new Client(name);
            client.Email = email;
            client.Status = MasterStatus.noStatus;
         

            var result = await this._user.CreateAsync(client, password);

            if (result.Succeeded)
            {
              
                return RedirectToAction("Login", "Account");
            }


            return RedirectToAction("Login", "Account");


        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await this._user.FindByNameAsync(userName);
            System.Console.WriteLine(user.Email);
            if (user != null)
            {

                var result = await this._signInManager.PasswordSignInAsync(user, password, false, false);

                if (result.Succeeded) 
                {    await this._signInManager.SignInAsync(user, false);
                    return RedirectToAction("ClientDetail", "Home");
                }

            }

            return RedirectToAction("Login", "Account");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login","Account");
        }
    }
}