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
        public Item? SelectedCartItem { get; set; }
        public string? CartQuery { get; set; }
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
            NotifyPropertyChanged(nameof(CartItems));
        }

        public void PurchaseItem() {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.Model.Quantity >= 1;
                var updatedItem = _cartSvc.AddOrUpdate(SelectedItem.Model);
                if(updatedItem != null && shouldRefresh) 
                {
                    RefreshUX();
                }
            }
        }
        public void ReturnItem() {
            if (SelectedCartItem != null) {
                var shouldRefresh = SelectedCartItem/*.Model*/.Quantity >= 1;
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem/*.Model*/);
                if (updatedItem != null && shouldRefresh)
                {
                    RefreshUX();
                }
            }
        }
        public void Checkout()
        {

        }
        public ObservableCollection<Item?> CartItems
        {
            get
            {
                var filteredList = _cartSvc.CartItems.Where(p => p?.Product?.Name?.ToLower().Contains(CartQuery?.ToLower() ?? string.Empty) ?? false);
                return new ObservableCollection<Item?>(filteredList);
            }
        }
        public async Task<bool> Search()
        {
            var result = await _cartSvc.Search(CartQuery);
            NotifyPropertyChanged(nameof(CartItems));
            return true;
        }
    }
}
