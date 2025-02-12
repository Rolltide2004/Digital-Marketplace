using COP4870.Models;
using COP4870.Services;
using Maui.cop4870.ViewModels;

namespace Maui.cop4870.Views;

public partial class ProductDetails : ContentPage
{
	public ProductDetails()
	{
		InitializeComponent();
		BindingContext = new ProductViewModel();
	}
	private void GoBackClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//InventoryManagement");
	}
	private void OkClicked(object sender, EventArgs e)
	{
		var name = (BindingContext as ProductViewModel)?.Name;
        InventoryServiceProxy.Current.AddOrUpdate(new Product { item = name });
        Shell.Current.GoToAsync("//InventoryManagement");
    }
}