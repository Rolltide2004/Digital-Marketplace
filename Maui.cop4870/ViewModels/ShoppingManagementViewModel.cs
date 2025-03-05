using System.Collections.ObjectModel;
using COP4870.Models;
using COP4870.Services;

namespace Maui.cop4870.ViewModels
{
    public class ShoppingManagementViewModel
    {
        private InventoryServiceProxy _invSvc = InventoryServiceProxy.Current;
        public ObservableCollection<Item?> Inventory
        {
            get {
                return new ObservableCollection<Item?>(_invSvc.Products);
            }
        }
    }
}
