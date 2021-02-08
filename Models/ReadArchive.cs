using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace nexo.Models
{
    public interface IReadArchive
    {
        void ReadJson();
    }

    public class ReadArchive :IReadArchive
    {
          

            public void ReadJson()
            {   
               var jason =  File.ReadAllText("product.json");

               var stuff = JsonConvert.DeserializeObject<List<Product>>(jason);

             
                foreach (var item in stuff)
                {
                    System.Console.WriteLine(item.Name);
                }
                
              

            }
    }
}