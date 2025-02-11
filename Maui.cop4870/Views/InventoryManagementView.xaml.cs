using Maui.cop4870.ViewModels;

namespace Maui.cop4870.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}