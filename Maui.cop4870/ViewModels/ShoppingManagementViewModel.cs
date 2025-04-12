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
        public ItemViewModel? SelectedItem { get; set; }
        public ItemViewModel? SelectedCartItem { get; set; }
        public ObservableCollection<ItemViewModel?> Inventory
        {
            get {
                return new ObservableCollection<ItemViewModel?>(_invSvc.Products
                    .Where(i => i?.Quantity > 0).Select(m => new ItemViewModel(m)));
            }
        }
        public ObservableCollection<ItemViewModel?> ShoppingCart
        {
            get
            {
                return new ObservableCollection<ItemViewModel?>(_cartSvc.CartItems
                    .Where(i => i?.Quantity > 0).Select(m => new ItemViewModel(m)));
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
        public void RefreshUX() {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }

        public void PurchaseItem() {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.Model.Quantity >= 1;
                var updatedItem = _cartSvc.AddOrUpdate(SelectedItem.Model);
                if(updatedItem != null && shouldRefresh) 
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }
        public void ReturnItem() {
            if (SelectedCartItem != null) {
                var shouldRefresh = SelectedCartItem.Model.Quantity >= 1;
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem.Model);
                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }
    }
}
