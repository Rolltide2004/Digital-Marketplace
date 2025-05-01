using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using COP4870.Models;
using COP4870.Util;
using COP4870.Utilities;
using Newtonsoft.Json;

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
            var productPayload = new WebRequestHandler().Get("/Cart").Result;
            items = JsonConvert.DeserializeObject<List<Item>>(productPayload) ?? new List<Item?>();
        }
        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
                if (existingInvItem.Quantity > 0)
                {
                    var rep = new WebRequestHandler().Post("/Inventory", existingInvItem).Result;
                    var oldItem = JsonConvert.DeserializeObject<Item>(rep);
                }
            }
            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                CartItems.Add(item);
            }
            else
            {
                item.Quantity = ++existingItem.Quantity;
            }
            var response = new WebRequestHandler().Post("/Cart", item).Result;
            var newItem = JsonConvert.DeserializeObject<Item>(response);
            return existingInvItem;
        }
        public Item? ReturnItem(Item? item)
        {
            if (item.Id <= 0 || item == null)
            {
                return null;
            }
            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity += itemToReturn.Quantity;
                    var rep = new WebRequestHandler().Post("/Inventory", inventoryItem).Result;
                    var oldItem = JsonConvert.DeserializeObject<Item>(rep);
                }
                itemToReturn.Quantity = 0;
            }
            var result = new WebRequestHandler().Delete($"/Cart/{item.Id}").Result;
            return JsonConvert.DeserializeObject<Item>(result);
        }
        public async Task<IEnumerable<Item?>> Search(string? query)
        {
            if (query != null)
            {
                return new List<Item>();
            }
            var response = await new WebRequestHandler().Post("/Cart/Search", new QueryRequest { Query = query });
            
            return items;
        }
        public void Delete()
        {
            var id = items.Where(i => i?.Quantity > 0).Select(p => p?.Id).ToList();
            foreach (var i in  id)
            {
                var result = new WebRequestHandler().Delete($"/Cart/{i}").Result;
            }
            items = new List<Item>();
        }
    }
}
