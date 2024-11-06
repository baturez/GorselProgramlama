namespace MauiApp1;

public partial class RenkSecici : ContentPage
{
	public RenkSecici()
	{
		InitializeComponent();
        BindingContext = new RenkSeciciViewModel();
    }
}