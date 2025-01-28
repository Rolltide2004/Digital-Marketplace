using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COP4870.Models
{
    public class Product
    {
        public string? item { get; set; }
        public int id { get; set; }

        public Product(){
            item = string.Empty;
        }
        public Product(string? i, int tempInt){
            item = i;
            id = tempInt;
        }
        public string? Display { 
            get { return $"{id}. {item}"; }
        }
        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
