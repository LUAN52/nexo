namespace nexo.Models
{
    public class Product
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }

        public string Client_id {get;set;}

        public double Price { get; set; }

        public Client Client {get;set;}


    }
}