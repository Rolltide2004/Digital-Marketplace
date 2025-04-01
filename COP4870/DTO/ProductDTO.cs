using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COP4870.Models;

namespace COP4870.DTO
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public ProductDTO()
        {
            Name = string.Empty;
        }
        public ProductDTO(Product p) {
            Name = p.Name;
            Id = p.Id;
        }
        public ProductDTO(string? i, int q, double p)
        {
            Name = i;
            Quantity = q;
            Price = p;
        }
        public string? Display
        {
            get { return $" {Id}. {Name}"; }//\t{Quantity}\t\t$ {Price}"; }
        }
        public override string ToString()
        {
            return Display ?? string.Empty;
        }
        public ProductDTO(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
        }
    }
}
