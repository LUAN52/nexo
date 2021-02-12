using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Register(string userName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Register", "Account");
            }

            var client = new Client(userName);
            client.Email = email;
         

            var result = await this._user.CreateAsync(client, password);

            if (result.Succeeded)
            {
                await this._signInManager.SignInAsync(client, false);
                return RedirectToAction("Login", "Account");
            }


            return RedirectToAction("Account", "Register");


        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await this._user.FindByNameAsync(userName);
            System.Console.WriteLine(user.Email);
            if (user != null)
            {

                if (DateTime.Now.Year - user.RegisterDate.Year >= 5)
                {
                    user.Status = MasterStatus.silver;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }

                var quantProds = _context.Products
                .Include(p => p.Client)
                .Where(p => p.Client_id == user.Id)
                .ToList()
                .Count();


                if (quantProds >= 2000)
                {
                    if ((quantProds >= 2000) && (quantProds < 5000))
                    {
                        user.Status = MasterStatus.gold;
                        _context.Users.Update(user);
                        _context.SaveChanges();
                    }
                    else
                    {
                        if ((quantProds >= 5000) && (quantProds < 10000))
                        {
                            user.Status = MasterStatus.platina;
                            _context.Users.Update(user);
                            _context.SaveChanges();
                        }
                        else
                        {
                            if (quantProds >= 10000)
                            {
                                user.Status = MasterStatus.diamond;
                                _context.Users.Update(user);
                                _context.SaveChanges();
                            }

                        }
                    }
                }
                else
                {
                    user.Status = MasterStatus.noStatus;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }


                var result = await this._signInManager.PasswordSignInAsync(user, password, false, false);

                if ((result.Succeeded) || (User.Identity.IsAuthenticated))
                {
                    return RedirectToAction("ProductList", "Home");
                }

            }

            return RedirectToAction("Login", "Account");
        }
    }
}