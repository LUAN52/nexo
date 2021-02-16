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
using nexo.repository;
using Nexo.data;
using System.Text.RegularExpressions;

namespace nexo.Controllers

{
    [Authorize]
    public class HomeController : Controller
    {


        private readonly AppDbContext _context;

        private readonly UserManager<Client> _userManager;
        private readonly ClientRepository _rClient;
        private readonly ProductRepository _rProduct;

        public HomeController(ClientRepository rClient,
        ProductRepository rProduct,
         UserManager<Client> userManager,
         AppDbContext context)
        {

            _userManager = userManager;
            _rClient = rClient;
            _rProduct = rProduct;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        [HttpGet]
        public IActionResult ProductList()
        {
            var id = _userManager.GetUserId(HttpContext.User);
            var listaProd = _rProduct.GetByIdClient(id);



            return View(listaProd);
        }

        [HttpGet]
        public IActionResult ClientList()
        {
            var clients = _rClient.GetAll();

            return View(clients);
        }

        [HttpGet]
        public IActionResult MasterList()
        {
            
            var clientsM = _rClient.Query(p=>p.Status!=MasterStatus.noStatus);
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


            var prod = _rProduct.GetById(id.ToString());

            return View(prod);

        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var prod = _context.Products
         .FirstOrDefault(p => p.id == id);

            return View(prod);
        }

        [HttpPost]
        public IActionResult DeleteProd(int id)
        {
            
            var prod = _rProduct.GetById(id.ToString());

            if (prod != null)
            {
                _rProduct.Delete(prod);
            }

            return RedirectToAction("ProductList", "Home");
        }

        [HttpPost]
        public IActionResult UpdateProduct(string productName, string price, int id)
        {
            var idUser = _userManager.GetUserId(HttpContext.User);

            var prod = _rProduct.GetOneProductByIdClient(id, idUser);

            var priceR = price.Replace(".", ",");
            var priceD = double.Parse(priceR);

            prod.Name = productName;
            prod.Price = priceD;


            _rProduct.Update(prod);

            return RedirectToAction("ProductList", "Home");

        }


        [HttpPost]
        public  IActionResult AddProduct(string productName, string price)
        {

            string priceR;
            double priceD;
            

            if ((productName != null && (price != null)))
            {

                System.Console.WriteLine(productName);
                priceR = price.Replace(".", ",");
                priceD = double.Parse(priceR);


                var id = _userManager.GetUserId(HttpContext.User);
                var prod = new Product
                {
                    Name = productName,
                    Price = priceD,
                    Available = true,
                    Client_id = id

                };

                _rProduct.insert(prod);
                var user = _rClient.GetById(id);

                if (DateTime.Now.Year - user.RegisterDate.Year >= 5)
                {
                    user.Status = MasterStatus.silver;
                   _rClient.Update(user);
                }

                var quantProds = _rProduct.GetByIdClient(id);
                
                if(quantProds.Count >= 2000)
                {
                    if ((quantProds.Count >= 2000) && (quantProds.Count < 5000))
                    {
                        user.Status = MasterStatus.gold;
                        _rClient.Update(user);
                    }
                    else
                    {
                        if ((quantProds.Count >= 5000) && (quantProds.Count < 10000))
                        {
                            user.Status = MasterStatus.platina;
                           _rClient.Update(user);
                        }
                        else
                        {
                            if (quantProds.Count >= 10000)
                            {
                                user.Status = MasterStatus.diamond;
                                _rClient.Update(user);
                            }

                        }
                    }
                }
               
                    return RedirectToAction("ProductList", "Home");
                

            }

            return RedirectToAction("AddProduct", "Home");
        }


        public IActionResult ClientDetail()
        {
              var id = _userManager.GetUserId(HttpContext.User);
              var user = _rClient.GetById(id);

              return View(user);
        }


        [HttpGet]
        public IActionResult GetProductByName(string productName)
        {
            var prod = _rProduct.Query(y=>y.Name==productName).FirstOrDefault();
            if(prod!=null)
            {
                  return View(prod);
            }

            return RedirectToAction("GetProduct","Home");
          

        }

        public IActionResult GetProduct()
        {
            return View();
        }


    }
}


