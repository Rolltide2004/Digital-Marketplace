using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COP4870.Models;

namespace COP4870.Services
{
    public class InventoryServiceProxy
    {
        private InventoryServiceProxy(){
            Products = new List<Product?> {
                new Product{id=1, item="Product 1", quantity=10, price=1.99 },
                new Product{id=2, item="Product 2", quantity=10, price=1.99 },
                new Product{id=3, item="Product 3", quantity=10, price=1.99 },
            };
        }
        
        private int LastKey{
            get{
                if (!Products.Any()){
                    return 0;
                }

                return Products.Select(p => p?.id ?? 0).Max();
            }
        }
        
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();
        
        public static InventoryServiceProxy Current{
            get{
                lock (instanceLock){
                    if (instance == null){
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }
        
        public List<Product?> Products { get; private set; }
        
        public Product AddOrUpdate(Product product){
            if (product.id == 0){
                product.id = LastKey + 1;
                Products.Add(product);
            }
            return product;
        }
        
        public Product? Delete(int id){
            if (id == 0){
                return null;
            }
            Product? product = Products.FirstOrDefault(p => p.id == id);
            Products.Remove(product);
            return product;
        }
        public Product? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.id == id);
        }
    }
}
