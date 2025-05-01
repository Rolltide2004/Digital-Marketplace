using Maui.cop4870.ViewModels;

namespace Maui.cop4870.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}
    private void AddToCartClicked(object sender, EventArgs e)
    {
		(BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }
    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnItem();
    }
    private void InlineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).RefreshUX();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void CheckoutClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).Checkout();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel)?.Search();
    }
}