using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COP4870.Models;
using COP4870.Services;

namespace Maui.cop4870.ViewModels
{
    public class ProductViewModel
    {
        public string? Name
        {
            get {
                return Model?.Product.Name ?? string.Empty;
            }
            set {
                if (Model!=null && Model?.Product.Name != value) {
                    Model.Product.Name = value;
                }
            }
        }
        public int? Quantity { 
            get {
                return Model?.Quantity;
            }
            set {
                if (Model != null && value!=null && Model.Quantity != value) {
                    Model.Quantity = value;
                }
            }
        }
        public Item? Model { get; set; }
        public void AddOrUpdate() {
            InventoryServiceProxy.Current.AddOrUpdate(Model);
        }
        public ProductViewModel() {
            Model = new Item();
        }
        public ProductViewModel(Item? model) {
            Model = model;
        }
    }
}
