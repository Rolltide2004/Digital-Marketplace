using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COP4870.Models;
using COP4870.Services;


namespace Maui.cop4870.ViewModels
{
    public class InventoryManagementViewModel
    {
        public Product? SelectedProduct { get; set; }
        private InventoryServiceProxy _svc = InventoryServiceProxy.Current;
        public List<Product?> Products
        {
            get {
                return _svc.Products;
            }
        }
        public Product? Delete()
        {
            return _svc.Delete(SelectedProduct?.id ?? 0);
        }
    }
}