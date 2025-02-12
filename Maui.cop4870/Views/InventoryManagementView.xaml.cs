using COP4870.Services;
using Maui.cop4870.ViewModels;

namespace Maui.cop4870.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Delete();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}