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
}