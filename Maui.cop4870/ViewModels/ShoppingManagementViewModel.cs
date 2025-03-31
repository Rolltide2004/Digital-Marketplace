using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using COP4870.Models;
using COP4870.Services;

namespace Maui.cop4870.ViewModels
{
    public class ShoppingManagementViewModel : INotifyPropertyChanged
    {
        private InventoryServiceProxy _invSvc = InventoryServiceProxy.Current;
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        public Item? SelectedItem { get; set; }
        public Item? SelectedCartItem { get; set; }
        public ObservableCollection<Item?> Inventory
        {
            get {
                return new ObservableCollection<Item?>(_invSvc.Products
                    .Where(i => i?.Quantity > 0));
            }
        }
        public ObservableCollection<Item?> ShoppingCart
        {
            get
            {
                return new ObservableCollection<Item?>(_cartSvc.CartItems
                    .Where(i => i?.Quantity > 0));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PurchaseItem() {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.Quantity >= 1;
                var updatedItem = _cartSvc.AddOrUpdate(SelectedItem);
                if(updatedItem != null && shouldRefresh) 
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }
        public void ReturnItem() {
            if (SelectedItem != null) {
                var shouldRefresh = SelectedCartItem.Quantity >= 1;
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem);
                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }
    }
}
