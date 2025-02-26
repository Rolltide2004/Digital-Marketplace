using COP4870.Models;
using COP4870.Services;
using Maui.cop4870.ViewModels;

namespace Maui.cop4870.Views;

[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductDetails : ContentPage
{
	public ProductDetails()
	{
		InitializeComponent();
	}
	public int ProductId { get; set; }
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

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		if (ProductId == 0)
		{
			BindingContext = new ProductViewModel();
		}
		else
		{
			BindingContext = new ProductViewModel(InventoryServiceProxy.Current.GetById(ProductId));
        }
	}
}