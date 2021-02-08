using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace nexo.Models
{
    public class Client : IdentityUser
    {
        public Client()
        {

        }

        public Client(string userName) : base(userName)
        {
            this.Products = new List<Product>();
        }

        public string LastName  { get; set; }

        public List<Product> Products {get;set;}

        public DateTime RegisterDate{ get; set; }

        public  MasterStatus Status { get; set; }

    }
}