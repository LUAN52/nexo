using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using nexo.Models;
using Nexo.data;

namespace nexo.Controllers

{   
    [Authorize]
    public class HomeController : Controller
    {

        
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        private readonly UserManager<Client> _userManager;

        public HomeController(ILogger<HomeController> logger,IReadArchive read, AppDbContext context,UserManager<Client> userManager)
        {
            _logger = logger;
           _context = context;
           _userManager = userManager;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProductList()
        {   
            var id = _userManager.GetUserId(HttpContext.User);
            var listaProd = _context.Products
           
            .Include(p=>p.Client)
            .Where(p=>p.Client_id==id)
            .ToList();
            
            return View(listaProd);
        }

        [HttpGet]
        public IActionResult ClientList()
        {
            var clients = _context.Users.ToList();

            return View(clients);
        }

        [HttpGet]
         public IActionResult MasterList()
        {
            var clientsM = _context.Users
            .Where(p=>p.Status!=MasterStatus.noStatus)
            .ToList();

            return View(clientsM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]

        public IActionResult AddProduct()
        {
            return View();
        }


        [HttpGet]

        public IActionResult UpdateProduct(int id)
        {
            

           var prod= _context.Products
           .FirstOrDefault(p=>p.id==id);
            
            return View(prod);
            
        }


       

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
              var prod= _context.Products
           .FirstOrDefault(p=>p.id==id);

           return View(prod);
        }

        [HttpPost]
          public IActionResult DeleteProd(int id)
        {
              var prod= _context.Products
              .Where(p=>p.id==id)
            .FirstOrDefault();

            if(prod!=null)
            {
                _context.Products.Remove(prod);
                _context.SaveChanges();
            }

           

           return RedirectToAction("ProductList","Home");
        }

        [HttpPost]
        public IActionResult UpdateProduct(int id,string productName,string price)
        {
            var idUser = _userManager.GetUserId(HttpContext.User);

            var prod= _context.Products
           .Include(p=>p.Client)
           .Where(p=>p.Client_id==idUser)
           .FirstOrDefault(p=>p.id==id);

            

           var Nprod = prod;

            var priceR = price.Replace(".",",");
            var priceD = double.Parse(priceR);

            Nprod.Name = productName;
            Nprod.Price = priceD;
            
            _context.Entry(prod).State = EntityState.Modified;
            _context.Update(prod);
            _context.SaveChanges();

            return RedirectToAction("ProductList","Home");


        }


        [HttpPost]
        public async  Task<IActionResult> AddProduct(string productName,string price)
        {   

            var priceR = price.Replace(".",",");
            var priceD = double.Parse(priceR);
            
        

            var id = _userManager.GetUserId(HttpContext.User);
            var prod = new Product
            {
                Name = productName,
                Price = priceD,
                Available =true,
                Client_id = id
                
            };

           var result = await _context.Products.AddAsync(prod);

           if(result.State.ToString()=="Added")
           {    
               await _context.SaveChangesAsync();
               return RedirectToAction("ProductList","Home");
           }

           return RedirectToAction("AddProduct","Home");
        }
    }
}
