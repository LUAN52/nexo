using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using nexo.Models;
using nexo.repository;
using Nexo.data;


namespace Nexo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Client> _user;
        private readonly SignInManager<Client> _signInManager;
        private readonly AppDbContext _context;
        private readonly ClientRepository _rClient;

        public AccountController(UserManager<Client> user,
         SignInManager<Client> singManager,
         AppDbContext context, ClientRepository rClient)
        {
            _user = user;
            _signInManager = singManager;
            _context = context;
            _rClient = rClient;
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
        public async Task<bool> Register(string name, string email, string password)
        {
            if ((string.IsNullOrWhiteSpace(email)) || (string.IsNullOrWhiteSpace(name)))
            {

                return false;
            }



            var client = new Client(name);
            client.Email = email;
            client.Status = MasterStatus.noStatus;


            var result = await this._user.CreateAsync(client, password);

            if (result.Succeeded)
            {

                return true;
            }

            return false;

        }



        public bool TestEmail(string email)
        {
            var user = _rClient.Query(i => i.Email == email).FirstOrDefault();

            if (user != null)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public async Task<bool> Login(string userName, string password)
        {
            var user = await this._user.FindByNameAsync(userName);
            if (user != null)
            {

                var result = await this._signInManager.PasswordSignInAsync(user, password, false, false);

                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, false);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}