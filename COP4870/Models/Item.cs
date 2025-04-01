using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using COP4870.DTO;

namespace COP4870.Models
{
    public class Item
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Quantity { get; set; }

        public ICommand? AddCommand { get; set; }

        public override string ToString()
        {
            return Display ?? string.Empty;
            //return $"{Product.ToString()}\t\tQuantity:{Quantity}";
        }
        public string Display { 
            get {
                return $"{Product.ToString()}\t\t{Quantity}";
                //return Product?.Display ?? string.Empty;
            }   
        }
        public Item() {
            Product = new ProductDTO();
            Quantity = 0;

            AddCommand = null;
        }
        private void DoAdd() { 
        }
        public Item(Item i) {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;

            //AddCommand = new Command(DoAdd);
        }
    }
}
