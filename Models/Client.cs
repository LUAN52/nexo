using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            this.RegisterDate = DateTime.Now;
        }

        

        public List<Product> Products {get;set;}

        public DateTime RegisterDate{ get;  private set; }

        public  MasterStatus Status { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+",ErrorMessage = "Informe um email válido...")]
        public  override string Email { get => base.Email; set => base.Email = value; }

    }
}