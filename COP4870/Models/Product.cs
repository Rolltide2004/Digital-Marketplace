using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COP4870.DTO;

namespace COP4870.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Product(){
            Name = string.Empty;
        }
        public Product(string? i, int q, double p)
        {
            Name = i;
            Quantity = q;
            Price = p;
        }
        public string? Display {
            get { return $" {Name}"; }
        }
        public override string ToString()
        {
            return Display ?? string.Empty;
        }
        public Product(Product p) {
            Name = p.Name;
            Id = p.Id;
        }
        public Product(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
        }
    }
}
