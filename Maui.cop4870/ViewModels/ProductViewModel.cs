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
                return Model.name ?? string.Empty;
            }
            set {
                if (Model!=null && Model.name != value) {
                    Model.name = value;
                }
            }
        }
        public Product? Model { get; set; }
        public void AddOrUpdate() {
            InventoryServiceProxy.Current.AddOrUpdate(Model);
        }
        public ProductViewModel() {
            Model = new Product();
        }
        public ProductViewModel(Product? model) {
            Model = model;
        }
    }
}
