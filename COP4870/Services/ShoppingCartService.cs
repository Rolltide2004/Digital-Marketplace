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
        private InventoryServiceProxy _prodSvc = InventoryServiceProxy.Current;
        private List<Item> items;
        public List<Item> CartItems { 
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
            items = new List<Item>(); 
        }
        public Item? AddOrUpdate(Item item) 
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if(existingInvItem==null || existingInvItem.Quantity == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }
            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);
            } else
            {
                existingItem.Quantity++;
            }
            return existingInvItem;
        }
    }
}
