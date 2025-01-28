﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COP4870.Models;

namespace COP4870.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy(){
            Products = new List<Product?>();
        }
        
        private int LastKey{
            get{
                if (!Products.Any()){
                    return 0;
                }

                return Products.Select(p => p?.id ?? 0).Max();
            }
        }
        
        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        
        public static ProductServiceProxy Current{
            get{
                lock (instanceLock){
                    if (instance == null){
                        instance = new ProductServiceProxy();
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
    }
}
