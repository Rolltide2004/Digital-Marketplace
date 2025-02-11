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
        public int quantity { get; set; }
        public double price { get; set; }

        public Product(){
            item = string.Empty;
        }
        public Product(string? i, int q, double p)
        {
            item = i;
            quantity = q;
            price = p;
        }
        public string? Display { 
            get { return $" {id}. {item}\t{quantity}\t\t$ {price}"; }
        }
    }
}
