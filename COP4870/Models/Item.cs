using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using COP4870.DTO;
using COP4870.Services;

namespace COP4870.Models
{
    public class Item
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Quantity { get; set; }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
        public string Display { 
            get {
                return $"{Product}\t\t{Quantity}";
            }   
        }
        public Item() {
            Product = new ProductDTO();
            Quantity = 0;
        }

        public Item(Item i) {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
        }
    }
}
