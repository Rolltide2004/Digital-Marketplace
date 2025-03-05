using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using COP4870.Models;

namespace COP4870.Services
{
    public class ShoppingCartService
    {
        private List<Product> items;
        public List<Product> CartItems { 
            get { 
                return items; 
            } 
        }
        public static ShoppingCartService Current { 
            get {
                if (instance == null) {
                    instance = new ShoppingCartService();
                }
                return instance;
            } 
        }
        private static ShoppingCartService? instance;
        private ShoppingCartService() { 
            items = new List<Product>(); 
        }
    }
}
