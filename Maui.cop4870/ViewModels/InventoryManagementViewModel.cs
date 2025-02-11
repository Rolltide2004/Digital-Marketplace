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
        public List<Product?> Products
        {
            get {
                return InventoryServiceProxy.Current.Products;
            }
        }
    }
}