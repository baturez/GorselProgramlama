using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class VucudKitle : ContentPage
    {
        public VucudKitle()
        {
            InitializeComponent();
            BindingContext = new VucudKitleViewModel(); 
        }
    }
}
