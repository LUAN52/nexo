using System.ComponentModel.DataAnnotations;

namespace nexo.Models
{
    public class Product
    {
        public int id { get; set; }

        [Required(ErrorMessage="nae permititdo valor em branco")]
        public string Name { get; set; }
        public bool Available { get; set; }

        public string Client_id {get;set;}
        
        [Required(ErrorMessage="nao e permitido valor em branco")]
        [RegularExpression(@"\d",ErrorMessage = "Informe um valor valido...")]
        public double Price { get; set; }

        public Client Client {get;set;}


    }
}